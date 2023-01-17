using System.ComponentModel.DataAnnotations;

namespace FantasyLeagueProject.DTOs
{
    public class CreatePlayerDTO
    {

        [Required]

        public string Name { get; set; } = string.Empty;

        [Required]
        public double Cost { get; set; } = 0.0;


    }
}
