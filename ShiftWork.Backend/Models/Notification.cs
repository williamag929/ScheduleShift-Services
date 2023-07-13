using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftWork.Backend.Models
{
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid NotificationId { get; set; }   
        public string UserId {get;set;}  
        public string UserToNotify {get;set;}
        public string Type {get; set;}  
        public string Data {get; set;}
        public bool read {get; set;}
        public DateTime NotifyDate {get;set;}
    }
}