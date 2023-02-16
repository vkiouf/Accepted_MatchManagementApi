using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchManagementApi.Models;

[Table("MatchOdds")]
public class MatchOdds
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? MatchOddsID { get; set; }

    [Required]
    public int MatchId { get; set; }

    [Required]
    [StringLength(1, MinimumLength =1)]
    public string Specifier { get; set; }

    [Required]
    public double Odd { get; set; }

    public Match Match { get; }
}
