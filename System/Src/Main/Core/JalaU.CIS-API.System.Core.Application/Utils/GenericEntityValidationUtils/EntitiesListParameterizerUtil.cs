//-----------------------------------------------------------------------
// <copyright file="EntitiesListParameterizerUtil.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net;
using System.Reflection;
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Clase utilitaria para aplicar parámetros a una lista de entidades, como filtrado, ordenación y paginación.
/// </summary>
/// <typeparam name="T">El tipo de las entidades.</typeparam>
public class EntitiesListParameterizerUtil<T>(
    List<T> initialEntities,
    List<string> fieldsAllowedToBeOrderedBy
)
{
    private List<string> FieldsAllowedToOrderBy { get; set; } = fieldsAllowedToBeOrderedBy;

    private List<T> EntitiesToReturn { get; set; } = initialEntities;

    /// <summary>
    /// Aplica los parámetros de filtrado, ordenación y paginación a la lista de entidades.
    /// </summary>
    /// <param name="entityFilter">Filtro de entidades a aplicar.</param>
    /// <param name="getAllEntitiesRequestDTO">Objeto que contiene los parámetros de la solicitud.</param>
    /// <returns>La lista de entidades con los parámetros aplicados.</returns>
    public List<T> ApplyGetAllParameters(
        EntityFilter<T> entityFilter,
        GetAllEntitiesRequestDTO getAllEntitiesRequestDTO
    )
    {
        this.FilterTopics(
            entityFilter,
            getAllEntitiesRequestDTO.Filter,
            getAllEntitiesRequestDTO.Keyword
        );

        this.OrderEntities(getAllEntitiesRequestDTO.OrderBy, getAllEntitiesRequestDTO.Order);
        this.GetPaginatedTopics(
            getAllEntitiesRequestDTO.PageSize,
            getAllEntitiesRequestDTO.PageNumber
        );

        return this.EntitiesToReturn;
    }

    private void FilterTopics(EntityFilter<T> entityFilter, string filter, string keyword)
    {
        this.ValidateCorrectFiltering(filter, keyword);
        if (!string.IsNullOrEmpty(filter))
        {
            this.EntitiesToReturn = entityFilter.Filter(this.EntitiesToReturn, filter, keyword);
        }
    }

    private void ValidateCorrectFiltering(string filter, string keyword)
    {
        if (!string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(filter))
        {
            throw new WrongDataException(
                "errors",
                [
                    new(
                        (int)HttpStatusCode.UnprocessableContent,
                        $"The keyword {keyword} can't be used if the filter is empty."
                    )
                ]
            );
        }
    }

    private void OrderEntities(string orderBy, string order)
    {
        List<MessageLogDTO> messageLogDTOs = [];

        if (!string.IsNullOrEmpty(orderBy))
        {
            if (!this.FieldsAllowedToOrderBy.Contains(orderBy.ToLower()))
            {
                messageLogDTOs.Add(
                    new MessageLogDTO(
                        (int)HttpStatusCode.UnprocessableContent,
                        $"Invalid orderBy parameter. Field '{orderBy}' is not allowed for ordering."
                    )
                );
                throw new WrongDataException("errors", messageLogDTOs);
            }

            if (
                !order.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                && !order.Equals("desc", StringComparison.CurrentCultureIgnoreCase)
            )
            {
                messageLogDTOs.Add(
                    new MessageLogDTO(
                        (int)HttpStatusCode.UnprocessableContent,
                        "Invalid order parameter. Supported values are 'asc' and 'desc'."
                    )
                );
                throw new WrongDataException("errors", messageLogDTOs);
            }

            PropertyInfo? propertyInfo = typeof(T).GetProperty(
                orderBy,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
            );
            if (propertyInfo != null)
            {
                var entitySorter = new GenericSorter<T>();
                this.EntitiesToReturn = entitySorter.Sort(
                    this.EntitiesToReturn,
                    t => (IComparable)propertyInfo.GetValue(t)!,
                    order
                );
            }
        }
    }

    private void GetPaginatedTopics(int pageSize, int pageNumber)
    {
        int actualPageSize = pageSize <= 0 ? this.EntitiesToReturn.Count : pageSize;
        Console.WriteLine(actualPageSize);

        int startIndex = (pageNumber - 1) * actualPageSize;
        int endIndex = Math.Min(startIndex + actualPageSize, this.EntitiesToReturn.Count);

        this.EntitiesToReturn = this.EntitiesToReturn.GetRange(startIndex, endIndex - startIndex);
    }
}
