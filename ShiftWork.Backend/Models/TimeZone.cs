namespace ShiftWork.Backend.Models
{
    public class Time_Zone
    {
        public int Id { get; set; }
        public string ZoneName { get; set; }
        public string CountryCode { get; set; }
        public string Abbreviation { get; set; }
        public double Time_Start { get; set; }
        public int GmtOffset { get; set; }
        public string DST { get; set; }


    }
}
