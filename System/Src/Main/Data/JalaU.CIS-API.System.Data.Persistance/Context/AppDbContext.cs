﻿//-----------------------------------------------------------------------
// <copyright file="AppDbContext.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Data.Persistance;

using global::System.Diagnostics.CodeAnalysis;
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Initializes a new instance of the <see cref="AppDbContext"/> class.
/// </summary>
/// <param name="options">The options for configuring the context.</param>
[SuppressMessage(
    "TableNamesToLowerCase",
    "SA1300",
    Justification = "It's in lower case since the tables in the db are also in lowerCase."
)]
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets or sets the database set representing the topics table.
    /// </summary>
    public DbSet<Topic> topics { get; set; }

    /// <summary>
    /// Gets or sets the database set representing the ideas table.
    /// </summary>
    public DbSet<Idea> ideas { get; set; }

    /// <summary>
    /// Gets or sets the database set representing the votes table.
    /// </summary>
    public DbSet<Vote> votes { get; set; }
}
