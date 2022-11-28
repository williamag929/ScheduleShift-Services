namespace ShiftWork.Backend.Models
{
	public class Role
	{
		public string RoleId { get; set; }
		public string RoleName { get; set; }
		public List<UserRole> UserRole { get; set; }
	}


}
