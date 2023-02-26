using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerApi.Entities
{
    public class Project: BaseEntity
    {
        public string Name { get; set; }
        public int MembersNum { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        [JsonIgnore]
        public Subject Subject { get; set; }

        public int? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        [JsonIgnore]
        public User Manager { get; set; }

        [JsonIgnore]
        public List<TeamRole> TeamRoles { get; set; } = new();

        [JsonIgnore]
        public List<User> Participants { get; set; } = new();
    }
}
