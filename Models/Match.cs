using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchManagementApi.Models;

public enum Sports : byte
{
    Football = 1,
    Basketball = 2
}

[Table("Match")]
public class Match
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? MatchID { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "Date")]
    public DateTime MatchDate { get; set; }

    [Required]
    [StringLength(5, MinimumLength =5)]
    public string MatchTime { get; set; }

    [Required]
    [StringLength(50)]
    public string TeamA { get; set; }
    [Required]
    [StringLength(50)]
    public string TeamB { get; set; }

    [Required]
    [EnumDataType(typeof(Sports), ErrorMessage = "Sports only match 1 for Football and 2 for Basketball")]
    public Sports Sport { get; set; }

    public ICollection<MatchOdds>? MatchOddss { get; set; }
}

