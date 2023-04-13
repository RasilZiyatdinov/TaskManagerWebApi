
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerApi.Entities;

namespace TaskManagerWebApi.Entities
{
    public class StudentTask
    {
        [JsonIgnore]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual User? Student { get; set; }
        [JsonIgnore]
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual TaskEntity? Task { get; set; }

        //public int RoleId { get; set; }
        //public virtual RoleProject? Role { get; set; }

        public int HoursNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
