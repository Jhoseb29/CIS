//-----------------------------------------------------------------------
// <copyright file="DependencyInjection.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Provides extension methods for dependency injection related to application services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers application services for the specified types in the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IService<Topic>, TopicService>();
        services.AddScoped<AbstractValidator<Topic>, TopicValidatorUtil>();
        services.AddSingleton<EntityFilter<Topic>, TopicFilters>();

        services.AddScoped<IService<Idea>, IdeaService>();
        services.AddScoped<AbstractValidator<Idea>, IdeaValidatorUtil>();
        services.AddSingleton<EntityFilter<Idea>, IdeaFilters>();

        services.AddScoped<IService<Vote>, VoteService>();
        services.AddScoped<AbstractValidator<Vote>, VoteValidatorUtil>();
        services.AddSingleton<EntityFilter<Vote>, VoteFilters>();

        return services;
    }
}
