namespace ShiftWork.Backend.DTOs
{
    public class LoginDto
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime LastLogin { get; set; }
        public string AvatarUrl { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
