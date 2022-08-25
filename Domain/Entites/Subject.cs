using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entites
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Class { get; set; }

        [Required]
        public Language SubjectLanguage { get; set; }
    }
}
