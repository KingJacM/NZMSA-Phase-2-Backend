using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class SpeechFile
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool isVIP { get; set; }

    }
}
