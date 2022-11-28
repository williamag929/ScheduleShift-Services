namespace ShiftWork.Backend.Models
{
    public class TaskShift
    {
        public int TaskShiftId { get; set; }
        public string TaskShiftName { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}
