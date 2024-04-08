namespace JalaU.CIS_API.System.Core.Domain;

public class TopicRequestDTO : BaseRequestDTO
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public required DateTime Date { get; set; }

    public required List<string> Labels { get; set; }
}
