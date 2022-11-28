namespace ShiftWork.Backend.Models
{
    public class UserRole
    { 
        public string UserRoleId { get; set; }
        public string PersonId { get; set; }
        public string RoleId { get; set; }
        public Person Person { get; set; }
        public Role Role { get; set; }
        
    }
	
}
