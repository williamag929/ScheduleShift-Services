namespace ShiftWork.Backend.Models
{
    public class Area
    {
        public int AreaId { get; set; }
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public List<Schedule> Schedules { get; set; } 
    }
}
