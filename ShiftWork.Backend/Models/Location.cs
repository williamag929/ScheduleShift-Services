namespace ShiftWork.Backend.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
        public int? TimeZoneId { get; set; }
        public string GeoLocation { get; set; } = string.Empty;
        public string LocationAddress {get;set;} = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public DateTime Deleted { get; set; } = DateTime.UtcNow;
        public string LocationConfig {get;set;} = string.Empty;
    }
}
