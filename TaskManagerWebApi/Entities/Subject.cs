using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManagerApi.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        [JsonIgnore]
        public User Teacher { get; set; }
        [JsonIgnore]
        public List<Group> Groups { get; set; } = new();
    }
}
