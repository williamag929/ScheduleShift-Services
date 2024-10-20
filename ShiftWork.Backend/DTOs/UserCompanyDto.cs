namespace ShiftWork.Backend.Models
{
    public class UserCompanyDto
    {
        public int UserCompanyId {get;set; }
        public string CompanyId { get;set; } = string.Empty;
        public int UserProfileId { get;set; }
        public UserProfile Profile {get;set; } = new UserProfile();
    
    }
}