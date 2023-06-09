namespace ShiftWork.Backend.Models
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int UserStatus { get; set; }

    }
}
