using System.Text.Json.Serialization;

namespace TaskManagerApi.Entities
{
    public class TeamRole : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public List<Project> Projects { get; set; } = new();
    }
}
