namespace ShiftWork.Backend.Models
{
    public class ScheduleShift
    {
        public int ScheduleShiftId { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PersonId { get; set; }
        public int? ScheduleId { get; set; } 
        public int? AreaId { get; set; }
        public int? LocationId { get; set; }
        public string? GeoLocationStart { get; set; }
        public string? GeoLocationEnd { get; set; }
        public string CompanyId { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }
        public DateTime Deleted { get; set; }
        public string AvatarImageIn {get;set;} = string.Empty;
        public string AvatarImageOut {get;set;} = string.Empty;
    }
}
