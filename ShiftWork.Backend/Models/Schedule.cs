namespace ShiftWork.Backend.Models
{
	public class Schedule
	{
		public int ScheduleId { get; set; }
		public int PersonId { get; set; }
		public int TaskShiftId { get; set; }
		public string KeyCode { get; set; }
		public DateTime Scheduledate { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public int AreaId { get; set; }
		public int LocationId { get; set; }
		public string TagColor { get; set; }
		public Person Person { get; set; }
		public TaskShift TaskShift { get; set; }
		public Area Area { get; set; }
		public Location Location { get; set; }
		public string CompanyId { get; set; }
		public Company Company { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime Created { get; set; } = DateTime.UtcNow;
		public DateTime Updated { get; set; }
		public DateTime Deleted { get; set; }

	}
}
