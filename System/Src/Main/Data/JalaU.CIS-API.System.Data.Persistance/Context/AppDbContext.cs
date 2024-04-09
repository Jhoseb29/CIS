//-----------------------------------------------------------------------
// <copyright file="AppDbContext.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Data.Persistance;

using JalaU.CIS_API.System.Core.Domain;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Initializes a new instance of the <see cref="AppDbContext"/> class.
/// </summary>
/// <param name="options">The options for configuring the context.</param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets or sets the database set representing the topics table.
    /// </summary>
    public DbSet<Topic> Topics { get; set; }

    /// <summary>
    /// Gets or sets the database set representing the ideas table.
    /// </summary>
    public DbSet<Idea> Ideas { get; set; }

    /// <summary>
    /// Gets or sets the database set representing the votes table.
    /// </summary>
    public DbSet<Vote> Votes { get; set; }
}
