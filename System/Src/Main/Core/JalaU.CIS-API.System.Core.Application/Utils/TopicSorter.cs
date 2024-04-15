using System;
using System.Collections.Generic;
using System.Linq;
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application
{
    /// <summary>
    /// Utility class for sorting topics list.
    /// </summary>
    public class TopicSorter
    {
        /// <summary>
        /// Sorts the topics list based on the given criteria.
        /// </summary>
        /// <param name="topics">The list of topics to sort.</param>
        /// <param name="orderBy">The field to order by ("title" or "date").</param>
        /// <param name="order">The order direction ("asc" or "desc").</param>
        /// <returns>The sorted list of topics.</returns>
        public List<Topic> Sort(List<Topic> topics, string orderBy, string order)
        {
            switch (orderBy.ToLower())
            {
                case "title":
                    return order.ToLower() == "desc" ? SortByTitleDescending(topics) : SortByTitleAscending(topics);
                case "date":
                    return order.ToLower() == "desc" ? SortByDateDescending(topics) : SortByDateAscending(topics);
                default:
                    throw new ArgumentException("Invalid orderBy parameter. Supported values are 'title' and 'date'.");
            }
        }

        private List<Topic> SortByTitleAscending(List<Topic> topics)
        {
            return topics.OrderBy(t => t.Title).ToList();
        }

        private List<Topic> SortByTitleDescending(List<Topic> topics)
        {
            return topics.OrderByDescending(t => t.Title).ToList();
        }

        private List<Topic> SortByDateAscending(List<Topic> topics)
        {
            return topics.OrderBy(t => t.Date).ToList();
        }

        private List<Topic> SortByDateDescending(List<Topic> topics)
        {
            return topics.OrderByDescending(t => t.Date).ToList();
        }
    }
}