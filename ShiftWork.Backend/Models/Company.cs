using System.ComponentModel.DataAnnotations.Schema;


namespace ShiftWork.Backend.Models
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ExternalCode { get; set; }
        public string Config { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }
        public DateTime Deleted { get; set; }
    }
}
