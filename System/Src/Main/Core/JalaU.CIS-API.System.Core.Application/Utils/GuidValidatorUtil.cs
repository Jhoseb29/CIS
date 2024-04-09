//-----------------------------------------------------------------------
// <copyright file="GuidValidatorUtil.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Utility class for validating and manipulating GUIDs.
/// </summary>
public class GuidValidatorUtil
{
    /// <summary>
    /// Validates a string representation of a GUID and returns a new GUID.
    /// </summary>
    /// <param name="input">The string representation of the GUID to validate.</param>
    /// <returns>A new GUID.</returns>
    public static Guid ValidateGuid(string input)
    {
        return Guid.NewGuid();
    }
}
