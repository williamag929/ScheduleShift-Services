namespace ShiftWork.Backend.DTOs
{
    public class ScheduleDto
    {
		public int ScheduleId { get; set; }
		public int PersonId { get; set; }
		public int TaskShiftId { get; set; }
		public string KeyCode { get; set; } = string.Empty;
		public DateTime Scheduledate { get; set; }
		public string StartTime { get; set; } = string.Empty;
		public string EndTime { get; set; } = string.Empty;
		public int AreaId { get; set; }
		public int LocationId { get; set; }
		public string TagColor { get; set; } = string.Empty;
		public string CompanyId { get; set; } = string.Empty;
	}
}
