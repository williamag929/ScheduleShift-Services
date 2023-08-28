namespace ShiftWork.Backend.Models
{
    public class TaskShift
    {
        public int TaskShiftId { get; set; }
        public string TaskShiftName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public bool isSchedule {get;set;} = true;
        public DateTime TaskDate { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
        public int LocationId { get; set; } = 0;
        public int AreaId { get; set; } = 0;
        public int PersonId {get;set;} = 0;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy {get;set;} = 0;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public DateTime Deleted { get; set; }
    }
}
