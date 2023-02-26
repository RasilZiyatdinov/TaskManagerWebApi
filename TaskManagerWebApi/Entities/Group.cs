using System.Text.Json.Serialization;

namespace TaskManagerApi.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public List<User> Students { get; set; } = new();
        [JsonIgnore]
        public List<Subject> Subjects { get; set; } = new();
    }
}
