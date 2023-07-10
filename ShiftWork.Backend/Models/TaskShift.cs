namespace ShiftWork.Backend.Models
{
    public class TaskShift
    {
        public int TaskShiftId { get; set; }
        public string TaskShiftName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public DateTime TaskDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CompanyId { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public DateTime Deleted { get; set; }
    }
}
