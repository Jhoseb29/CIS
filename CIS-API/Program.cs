//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.Json.Serialization;
using JalaU.CIS_API.System.Api.Restful;
using JalaU.CIS_API.System.Core.Application;
using JalaU.CIS_API.System.Core.Domain;
using JalaU.CIS_API.System.Data.Persistance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySQLDatabaseConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddScoped<IService<Topic>, TopicService>();
builder.Services.AddScoped<IRepository<Topic>, TopicRepository>();
builder.Services.AddScoped<IValidator<Topic>, TopicValidatorUtil>();
builder.Services.AddSingleton<EntityFilter<Topic>, TopicFilters>();

builder.Services.AddScoped<IService<Idea>, IdeaService>();
builder.Services.AddScoped<IRepository<Idea>, IdeaRepository>();
builder.Services.AddScoped<IValidator<Idea>, IdeaValidatorUtil>();
builder.Services.AddSingleton<EntityFilter<Idea>, IdeaFilters>();

builder.Services.AddScoped<EnforceJsonResponseFilter>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
