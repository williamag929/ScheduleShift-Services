namespace ShiftWork.Backend.DTOs
{
    public class LocationDto
    {
        public int? LocationId { get; set; }
        public string? LocationName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? TimeZoneId { get; set; }
        public string GeoLocation { get; set; } = string.Empty;
        public string CompanyId { get; set; } = string.Empty;


    }
}
