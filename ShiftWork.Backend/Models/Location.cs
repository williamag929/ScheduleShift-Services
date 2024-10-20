namespace ShiftWork.Backend.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
        public int? TimeZoneId { get; set; }
        public string GeoLocation { get; set; } = string.Empty;
        public string Latitude  { get; set; } = string.Empty;
        public string Longitude  { get; set; } = string.Empty;
        public string Ration {get;set;} = string.Empty;
        public string LocationAddress {get;set;} = string.Empty;
        public string CityCode {get;set;} = string.Empty;
        public string StateCode {get;set;} = string.Empty;
        public string Zipcode {get;set;} = string.Empty;
        public string CountryCode {get;set;} = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
        public bool Notification {get;set;}= true;
        public bool ValidateOnSite {get;set;} = true;
        public bool ValidateRatio {get;set;} = true;
        public string RatioMax {get; set;} = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public DateTime Deleted { get; set; } = DateTime.UtcNow;
        public string LocationConfig {get;set;} = string.Empty;
    }
}
