using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Domain.Entites
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public Gender Sex { get; set; }

        public string? Image { get; set; }

        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }
    }
}
