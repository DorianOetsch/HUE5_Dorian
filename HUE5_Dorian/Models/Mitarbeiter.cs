using System.ComponentModel.DataAnnotations;

namespace HUE5_Dorian.Models
{
    public class Mitarbeiter
    {
        public int ID { get; set; }

        [Required]
        public string Firstname { get; set; } = string.Empty;
        [Required]
        public string Lastname { get; set; } = string.Empty;

    }
}
