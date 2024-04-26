//-----------------------------------------------------------------------
// <copyright file="GlobalVariables.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Static class for storing global variables used throughout the application.
/// </summary>
public static class GlobalVariables
{
    /// <summary>
    /// Gets or sets the user identifier stored in the "id" claim of the JWT.
    /// </summary>
    /// <remarks>
    /// This user identifier can be used in the application logic to reference the authenticated user in various operations.
    /// </remarks>
    public static string? UserId { get; set; }
}
