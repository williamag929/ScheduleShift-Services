
namespace ShiftWork.Backend.DTOs
{
    public class CompanyDto
    {
        public string CompanyId { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ExternalCode { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
