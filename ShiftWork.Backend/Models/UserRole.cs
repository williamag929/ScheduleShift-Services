using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftWork.Backend.Models
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        public int UserProfileId { get; set; }
        public string CompanyId { get; set; }  = string.Empty;
        public int RoleId { get; set; }
        public int PersonId { get; set; }
    }
	
}
