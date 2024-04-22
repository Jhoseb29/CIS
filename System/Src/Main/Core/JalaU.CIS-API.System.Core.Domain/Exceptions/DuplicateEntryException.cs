//-----------------------------------------------------------------------
// <copyright file="DuplicateEntryException.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents an exception that is thrown when an entity is not duplicated.
/// </summary>
public class DuplicateEntryException(string message) : ApplicationException(message) { }
