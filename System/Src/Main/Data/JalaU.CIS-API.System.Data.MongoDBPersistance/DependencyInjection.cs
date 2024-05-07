//-----------------------------------------------------------------------
// <copyright file="DependencyInjection.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace JalaU.CIS_API.System.Data.MongoDBPersistance;

/// <summary>
/// Provides extension methods for dependency injection related to MongoDB persistence.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers MongoDB persistence services for the specified types in the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddMongoDbPersistance(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Topic>, TopicRepository>();
        services.AddScoped<IRepository<Idea>, IdeaRepository>();
        services.AddScoped<IRepository<Vote>, VoteRepository>();

        return services;
    }
}
