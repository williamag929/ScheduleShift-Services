namespace ShiftWork.Backend.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}
