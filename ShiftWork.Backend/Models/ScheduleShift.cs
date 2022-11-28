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
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string? GeoLocationStart { get; set; }
        public string? GeoLocationEnd { get; set; }
        public DateTime Created { get; set; }   
        public DateTime Updated { get; set; }
        public DateTime Deleted { get; set; }
    }
}
