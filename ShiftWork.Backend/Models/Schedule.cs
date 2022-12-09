namespace ShiftWork.Backend.Models
{
	public class Schedule
	{
		public int ScheduleId { get; set; }
		public int PersonId { get; set; }
		public int? TaskShiftId { get; set; }
		public string KeyCode { get; set; }
		public DateTime Scheduledate { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public int LocationId { get; set; }
		public int? AreaId { get; set; }
		public string TagColor { get; set; }
		public string CompanyId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime Created { get; set; } = DateTime.UtcNow;
		public DateTime Updated { get; set; }
		public DateTime Deleted { get; set; }

	}
}
