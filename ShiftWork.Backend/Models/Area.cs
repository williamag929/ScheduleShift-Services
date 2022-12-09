namespace ShiftWork.Backend.Models
{
    public class Area
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public string CompanyId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }
        public DateTime Deleted { get; set; }
        public Location Location { get; set; }
        public Company Company { get; set; }

    }
}
