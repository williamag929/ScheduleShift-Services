namespace ShiftWork.Backend.DTOs
{
    public class TaskShiftDto
    {
        public int TaskShiftId { get; set; }
        public string TaskShiftName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public DateTime TaskDate { get; set; }
        public StartTime StartTime { get; set; }
        public StartTime EndTime { get; set; }
        public string CompanyId { get; set; } = string.Empty;
    }
}
