using System.ComponentModel.DataAnnotations;

namespace FantasyLeagueProject.DTOs
{
    public class LoginDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string? Username { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string? Password { get; set; } = string.Empty;

    }
}
