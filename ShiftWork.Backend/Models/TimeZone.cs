namespace ShiftWork.Backend.Models
{
    public class Time_Zone
    {
        public int Id { get; set; }
        public string ZoneName { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public double Time_Start { get; set; }
        public int GmtOffset { get; set; }
        public string DST { get; set; } = string.Empty;


    }
}
