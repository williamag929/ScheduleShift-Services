namespace ShiftWork.Backend.Models
{
	public class Role
	{
		public string RoleId { get; set; }
		public string RoleName { get; set; }
		public string CompanyId { get; set; }
		public Company Company { get; set; }

	}


}
