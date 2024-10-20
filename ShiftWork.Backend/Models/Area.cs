namespace ShiftWork.Backend.Models
{
    public class Area
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public string CompanyId { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public DateTime Deleted { get; set; } = DateTime.UtcNow;
    }
}
