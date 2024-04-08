using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JalaU.CIS_API.System.Core.Domain;

public class Idea
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public required Guid Id { get; set; }

    [Column("title")]
    [StringLength(200)]
    public required string Title { get; set; }

    [Column("description")]
    [StringLength(500)]
    public required string Description { get; set; }

    [Column("date")]
    public required DateTime Date { get; set; }

    [Column("userId")]
    public required Guid UserId { get; set; }

    [Column("topicId")]
    public required Guid TopicId { get; set; }
}
