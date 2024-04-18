//-----------------------------------------------------------------------
// <copyright file="GenericSorter.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application
{
    /// <summary>
    /// This class implements a generic sorter that can sort an array of elements of any type using the merge sort algorithm.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array to be sorted.</typeparam>
    public class GenericSorter<T>
    {
        /// <summary>
        /// Sorts the list of objects based on the given criteria.
        /// </summary>
        /// <param name="items">The list of objects to sort.</param>
        /// <param name="orderBy">The field to order by.</param>
        /// <param name="order">The order direction ("asc" or "desc").</param>
        /// <returns>The sorted list of objects.</returns>
        public List<T> Sort(List<T> items, Func<T, IComparable> orderBy, string order)
        {
            if (order.Equals("desc", StringComparison.CurrentCultureIgnoreCase))
            {
                return items.OrderByDescending(orderBy).ToList();
            }
            else if (order.Equals("asc", StringComparison.CurrentCultureIgnoreCase))
            {
                return [.. items.OrderBy(orderBy)];
            }
            else
            {
                throw new ArgumentException(
                    "Invalid order parameter. Supported values are 'asc' and 'desc'."
                );
            }
        }
    }
}
