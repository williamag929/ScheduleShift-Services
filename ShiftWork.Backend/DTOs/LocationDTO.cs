namespace ShiftWork.Backend.DTOs
{
    public class LocationDto
    {
        public int? LocationId { get; set; }
        public string? LocationName { get; set; }
        public string GeoLocation { get; set; } = string.Empty;
        public string Latitude  { get; set; } = string.Empty;
        public string Longitude  { get; set; } = string.Empty;
        public string CityCode {get;set;} = string.Empty;
        public string StateCode {get;set;} = string.Empty;
        public string Zipcode {get;set;} = string.Empty;
        public string CountryCode {get;set;} = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
        public string Ration {get;set;} = string.Empty;
        public string LocationAddress {get;set;} = string.Empty;
        public bool Notification {get;set;}= true;
        public bool ValidateOnSite {get;set;} = true;
        public bool ValidateRatio {get;set;} = true;
        public string RatioMax {get; set;} = string.Empty;


    }
}
