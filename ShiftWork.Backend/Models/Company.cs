using System.ComponentModel.DataAnnotations.Schema;


namespace ShiftWork.Backend.Models
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ExternalCode { get; set; } = string.Empty;
        public string Config { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public DateTime Deleted { get; set; }
    }
}
