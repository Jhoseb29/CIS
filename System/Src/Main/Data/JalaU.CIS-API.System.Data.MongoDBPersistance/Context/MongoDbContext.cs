//-----------------------------------------------------------------------
// <copyright file="MongoDbContext.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace JalaU.CIS_API.System.Data.MongoDBPersistance;

/// <summary>
/// Initializes a new instance of the <see cref="MongoDbContext"/> class.
/// </summary>
/// <param name="options">The options for configuring the context.</param>
[SuppressMessage(
    "TableNamesToLowerCase",
    "SA1300",
    Justification = "It's in lower case since the tables in the db are also in lowerCase."
)]
public class MongoDbContext(DbContextOptions<MongoDbContext> options) : DbContext(options)
{
    /// <summary>
    /// On Model Creating.
    /// </summary>
    /// <param name="builder">The builder for configuring the context.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Topic>().ToCollection("topics");
    }

    /// <summary>
    /// Gets or sets the database set representing the topics table.
    /// </summary>
    public DbSet<Topic> topics { get; set; }
}
