namespace ShiftWork.Backend.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = string.Empty;
        public string PermissionTag { get; set; } = string.Empty;  //group
        public string PermissionContext { get; set; } = string.Empty; //action
        public string OptionValues { get; set; } = string.Empty;  //none, readonly, standard, admin
        public string Extended { get; set; } = string.Empty;
    }
}
