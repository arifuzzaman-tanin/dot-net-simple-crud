using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Domain.Entites
{
    public class AdmittedStudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Age { get; set; }

        [Required]
        public Gender Sex { get; set; }

        public string? Image { get; set; }

        public int? Class { get; set; }

        [Required]
        [Display(Name="Roll No")]
        public string RollNo { get; set; }

        [NotMapped]
        [Display(Name="Image")]
        public IFormFile? ImageFile { get; set; }

    }
}
