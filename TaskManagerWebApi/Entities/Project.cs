using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using TaskManagerWebApi.Entities;
using Castle.Components.DictionaryAdapter;

namespace TaskManagerApi.Entities
{
    public class Project: BaseEntity
    {
        public string Name { get; set; }
        public int MembersNum { get; set; }
        [JsonIgnore]
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject? Subject { get; set; }

        [JsonIgnore]
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }
        [Column(TypeName = "Date")]
        public DateTime ExpirationDate { get; set; }

        [JsonIgnore]
        public int? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public virtual User? Manager { get; set; }
        //public virtual List<TeamRole>? TeamRoles { get; set; } = new();
        public virtual List<RoleProject>? TeamRoles { get; set; } = new();

        [JsonIgnore]
        public virtual List<User> RequestedParticipants { get; set; } = new();
        public virtual List<Request>? Requests { get; set; } = new();
        public virtual List<Mark>? Marks { get; set; } = new();

    }
}
