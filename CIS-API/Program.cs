//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.Json.Serialization;
using CIS_API;
using JalaU.CIS_API.System.Api.Restful;
using JalaU.CIS_API.System.Core.Application;
using JalaU.CIS_API.System.Core.Domain;
using JalaU.CIS_API.System.Data.MongoDBPersistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MongoDBAtlasDatabaseConnection");

builder.Services.AddDbContext<MongoDbContext>(
    options => options.UseMongoDB(connectionString!, "CIS_API")
);

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddApplication().AddMongoDbPersistance();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddScoped<EnforceJsonResponseFilter>();

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Convert.FromBase64String(builder.Configuration["JWT:Key"]!)
            ),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description =
                "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
        }
    );

    config.AddSecurityRequirement(
        new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            },
        }
    );
});

var app = builder.Build();

app.UseExceptionHandler("/error");
app.UseAuthentication();
app.UseAuthorization();
app.Use(
    async (context, next) =>
    {
        var userIdClaim = context.User.FindFirst("id");
        if (userIdClaim != null)
        {
            GlobalVariables.UserId = userIdClaim.Value;
        }

        await next.Invoke();
    }
);
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
