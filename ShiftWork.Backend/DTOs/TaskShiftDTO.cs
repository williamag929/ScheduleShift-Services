namespace ShiftWork.Backend.DTOs
{
    public class TaskShiftDto
    {
        public int TaskShiftId { get; set; }
        public string TaskShiftName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CompanyId { get; set; } = string.Empty;
    }
}
