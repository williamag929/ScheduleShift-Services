namespace ShiftWork.Backend.Models
{
    public class TaskShift
    {
        public int TaskShiftId { get; set; }
        public string TaskShiftName { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Schedule> Schedules { get; set; }
        public string CompanyId { get; set; }
        public Company Company { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }
        public DateTime Deleted { get; set; }
    }
}
