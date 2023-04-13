using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerWebApi.Entities;

namespace TaskManagerApi.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string Name { get; set; }

        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set;}

        [Column(TypeName = "Date")]
        public DateTime ExpirationDate { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set;}
        [JsonIgnore]
        public int PriorityId { get; set; }
        [ForeignKey("PriorityId")]
        public virtual Priority Priority { get; set;}
        [JsonIgnore]
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        [JsonIgnore]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [JsonIgnore]
        public int TeamRoleId { get; set; }
        [ForeignKey("TeamRoleId")]
        public virtual RoleProject TeamRole { get; set; }


        public virtual List<User>? Students { get; set; }
        public virtual List<StudentTask>? StudentTasks { get; set; } = new();
        public virtual List<Comment>? Comments { get; set; } = new();

    }
}
