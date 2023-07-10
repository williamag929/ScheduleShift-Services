namespace ShiftWork.Backend.DTOs
{
    public class PersonDto
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public int ManagerId { get; set; }
        public bool isManager { get; set; }
        public bool isSchedule { get; set; }
        public string Email { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public string CompanyId { get; set; } = string.Empty;
    }
}
