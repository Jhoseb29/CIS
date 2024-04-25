//-----------------------------------------------------------------------
// <copyright file="GetAllEntitiesRequestDTO.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Base class for request data transfer objects (DTOs).
/// </summary>
public class GetAllEntitiesRequestDTO
{
    /// <summary>
    /// Gets or sets the size of each page in the paginated result. Can be null to indicate no paging.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the page number of the paginated result.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the field by which to order the results.
    /// </summary>
    public required string OrderBy { get; set; }

    /// <summary>
    /// Gets or sets the order direction ('asc' for ascending, 'desc' for descending).
    /// </summary>
    public required string Order { get; set; }

    /// <summary>
    /// Gets or sets the filter criteria to apply to the query.
    /// </summary>
    public required string Filter { get; set; }

    /// <summary>
    /// Gets or sets the keyword to search for in the entities.
    /// </summary>
    public required string Keyword { get; set; }
}
