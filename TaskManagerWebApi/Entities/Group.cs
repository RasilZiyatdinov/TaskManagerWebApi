using System.Text.Json.Serialization;

namespace TaskManagerApi.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public virtual List<User> Students { get; set; } = new();
        [JsonIgnore]
        public virtual List<Subject> Subjects { get; set; } = new();
    }
}
