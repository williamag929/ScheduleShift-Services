using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftWork.Backend.Models
{
    public class EventLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EventLogId { get; set; }
        public DateTime EventDate {get;set;}
        public string EventType {get;set;}
        public int UserId {get;set;}
        public string EventObject {get;set;}
        public string EventObjectId {get;set;}
        public string Description {get;set;}
    }
}