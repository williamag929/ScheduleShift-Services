namespace ShiftWork.Backend.DTOs
{
    public class LocationDto
    {
        public int? LocationId { get; set; }
        public string? LocationName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? TimeZoneId { get; set; }
        public string GeoLocation { get; set; }
        public string CompanyId { get; set; }


    }
}
