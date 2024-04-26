//-----------------------------------------------------------------------
// <copyright file="DependencyInjection.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace JalaU.CIS_API.System.Data.Persistance;

/// <summary>
/// Provides extension methods for dependency injection related to MySQL persistence.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers MySQL persistence services for the specified types in the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddMySQLPersistance(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Topic>, TopicRepository>();
        services.AddScoped<IRepository<Idea>, IdeaRepository>();
        services.AddScoped<IRepository<Vote>, VoteRepository>();

        return services;
    }
}
