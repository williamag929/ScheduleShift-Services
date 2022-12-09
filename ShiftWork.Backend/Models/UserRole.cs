namespace ShiftWork.Backend.Models
{
    public class UserRole
    { 
        public string UserRoleId { get; set; }
        public int UserProfileId { get; set; }
        public string CompanyId { get; set; }
        public string RoleId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public Role Role { get; set; }
        public UserProfile Profile { get; set; }
        public Company Company { get; set; }
    }
	
}
