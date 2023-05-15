namespace ShiftWork.Backend.DTOs
{
    public class AreaDto
    {
        public int? AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
        public int LocationId { get; set; }
    }
}

