namespace ShiftWork.Backend.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public int ManagerId { get; set; } = 0;
        public bool isManager { get; set; } = false;
        public bool isSchedule { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string Email { get; set; }    = string.Empty;
        public int PhoneNumber { get; set; } = 0;
        public string MainAddreess {get;set;} = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public DateTime Deleted { get; set; } = DateTime.UtcNow;
        public string PersonConfig {get;set; } = string.Empty;
        public string PrivateKey {get;set;} = string.Empty;
        public string AvatarImage {get;set;} = string.Empty;

    }
}
