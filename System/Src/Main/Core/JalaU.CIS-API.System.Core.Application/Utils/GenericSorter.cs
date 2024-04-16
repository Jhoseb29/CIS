using System;
using System.Collections.Generic;
using System.Linq;
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application
{
    /// <summary>
    /// Utility class for sorting lists of objects.
    /// </summary>
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
            if (order.ToLower() == "desc")
            {
                return items.OrderByDescending(orderBy).ToList();
            }
            else if (order.ToLower() == "asc")
            {
                return items.OrderBy(orderBy).ToList();
            }
            else
            {
                throw new ArgumentException("Invalid order parameter. Supported values are 'asc' and 'desc'.");
            }
        }
    }
}