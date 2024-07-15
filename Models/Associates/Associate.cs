using BringMeBackAPI.Models.Reports;
using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Associates
{
    public class Associate
    {
        public int AssociateId { get; set; }

        [Required]
        public int ReportId { get; set; }
        public Report Report { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Relationship is required.")]
        [MaxLength(50, ErrorMessage = "Relationship cannot exceed 50 characters.")]
        public string Relationship { get; set; }
    }


}
