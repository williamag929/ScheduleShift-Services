namespace ShiftWork.Backend.Models
{
	public class Schedule
	{
		public int ScheduleId { get; set; }
		public int PersonId { get; set; }
		public int? TaskShiftId { get; set; }
		public string KeyCode { get; set; } = string.Empty;
		public DateTime Scheduledate { get; set; }
		public string StartTime { get; set; } = string.Empty;
		public string EndTime { get; set; } = string.Empty;
		public int LocationId { get; set; }
		public int? AreaId { get; set; }
		public string TagColor { get; set; } = string.Empty;
		public string CompanyId { get; set; } = string.Empty;
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsApproved { get; set; }
		public DateTime Created { get; set; } = DateTime.UtcNow;
		public DateTime Updated { get; set; }
		public DateTime Deleted { get; set; }

	}
}
