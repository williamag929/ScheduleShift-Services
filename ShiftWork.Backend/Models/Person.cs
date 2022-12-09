namespace ShiftWork.Backend.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public int ManagerId { get; set; }
        public bool isManager { get; set; }
        public bool isSchedule { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }   
        public int PhoneNumber { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<UserRole> UserRole { get; set; }
        public string CompanyId { get; set; }
        public Company Company { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }
        public DateTime Deleted { get; set; }

    }
}
