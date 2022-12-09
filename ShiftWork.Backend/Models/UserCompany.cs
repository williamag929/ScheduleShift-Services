namespace ShiftWork.Backend.Models
{
    public class UserCompany
    {
        public int UserCompanyId {get;set; }
        public string CompanyId { get;set; }
        public int UserProfileId { get;set; }
        public UserProfile Profile {get;set; }  
    
    }
}
