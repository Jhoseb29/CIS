//-----------------------------------------------------------------------
// <copyright file="CreateTopicService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using JalaU.CIS_API.System.Core.Domain;
using JalaU.CIS_API.System.Data.Persistance;

namespace JalaU.CIS_API.System.Core.Application.Services
{
    /// <summary>
    /// Service for creating topics in the database.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CreateTopicService"/> class.
    /// </remarks>
    /// <param name="context">The database context.</param>
    internal class CreateTopicService(AppDbContext context)
    {
        private readonly AppDbContext context = context;
        private ValidatorCreateTopic validator;

        /// <summary>
        /// Creates a new topic in the database.
        /// </summary>
        /// <param name="topic">The topic to create.</param>
        /// <returns>The DTO of the created topic.</returns>
        public async Task<TopicRequestDTO?> CreateTopic(Topic topic)
        {
            TopicRequestDTO? topicDTO = null;
            if (this.validator.NullableFields(topic).Count == 0)
            {
                topicDTO = this.validator.ConverDTO(topic);
            }

            this.context.Topics.Add(topic);
            await this.context.SaveChangesAsync();

            return topicDTO;
        }

        /// <summary>
        /// Retrieves errors in fields of a topic.
        /// </summary>
        /// <param name="topic">The topic to check for errors.</param>
        /// <returns>List of errors found in fields.</returns>
        public List<string> ErrorsInFields(Topic topic)
        {
            return this.validator.NullableFields(topic);
        }
    }
}
