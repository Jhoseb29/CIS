//-----------------------------------------------------------------------
// <copyright file="ValidatorCreateTopic.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Reflection;
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Class ValidatorCreateTopic responsible for validating topic creation.
/// </summary>
public class ValidatorCreateTopic
{
    private readonly BadWords badWords = new BadWords();

    /// <summary>
    /// Checks if there are any bad words in the title or description of a given topic.
    /// </summary>
    /// <param name="topic">The topic to validate.</param>
    /// <returns>True if there are any bad words in the title or description of the topic; otherwise, false.</returns>
    public bool AreThereAnyBadWord(Topic topic)
    {
        return this.badWords.BadWordsInText(topic.Title) && this.badWords.BadWordsInText(topic.Description);
    }

    /// <summary>
    /// Convierte un objeto Topic en un DTO (Data Transfer Object).
    /// </summary>
    /// <param name="topic">El topico a convertir.</param>
    /// <returns>El DTO del topico.</returns>
    public TopicRequestDTO ConverDTO(Topic topic)
    {
        TopicRequestDTO topicDTO = new TopicRequestDTO();
        topicDTO.Title = topic.Title;
        topicDTO.Description = topic.Description;
        topicDTO.Date = topic.Date;
        topicDTO.Labels = topic.Labels;

        return topicDTO;
    }

    /// <summary>
    /// verifica los campos nulos o vacíos de un objeto Topic.
    /// </summary>
    /// <param name="topic">El topico a verificar.</param>
    /// <returns>Una lista de nombres de campos nulos o vacíos.</returns>
    public List<string> NullableFields(Topic topic)
    {
        List<string> nullFields = new List<string>();
        Type tipo = typeof(Topic);
        PropertyInfo[] properties = tipo.GetProperties();
        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(topic);
            if ((value == null || string.IsNullOrEmpty(value.ToString())) && property.Name != "Id" && property.Name != "UserId")
            {
                nullFields.Add(property.Name);
            }
        }

        return nullFields;
    }


}