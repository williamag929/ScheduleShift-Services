namespace ShiftWork.Backend.DTOs
{
    public class TaskShiftDto
    {
        public int TaskShiftId { get; set; }
        public string TaskShiftName { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
