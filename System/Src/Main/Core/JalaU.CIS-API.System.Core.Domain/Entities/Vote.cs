using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JalaU.CIS_API.System.Core.Domain;

public class Vote
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public required Guid Id { get; set; }

    [Column("positive")]
    public required bool Positive { get; set; }

    [Column("userId")]
    public required Guid UserId { get; set; }

    [Column("ideaId")]
    public required Guid IdeaId { get; set; }
}
