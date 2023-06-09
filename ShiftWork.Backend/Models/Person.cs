namespace ShiftWork.Backend.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public int ManagerId { get; set; }
        public bool isManager { get; set; }
        public bool isSchedule { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }    = string.Empty;
        public int PhoneNumber { get; set; }
        public string CompanyId { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }
        public DateTime Deleted { get; set; }

    }
}
