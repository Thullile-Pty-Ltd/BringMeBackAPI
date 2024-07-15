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

        // Link to other types of reports if needed (e.g., ItemReport)
        public int OtherReportId { get; set; }
        public Report OtherReport { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Relationship is required.")]
        [MaxLength(50, ErrorMessage = "Relationship cannot exceed 50 characters.")]
        public string Relationship { get; set; }
    }


}
