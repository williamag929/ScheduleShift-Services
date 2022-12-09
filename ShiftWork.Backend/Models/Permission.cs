namespace ShiftWork.Backend.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionTag { get; set; }  //group
        public string PermissionContext { get; set; }  //action
        public string OptionValues { get; set; }  //none, readonly, standard, admin
        public string Extended { get; set; }
    }
}
