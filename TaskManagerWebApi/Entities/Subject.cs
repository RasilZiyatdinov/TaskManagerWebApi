using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManagerApi.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        //[JsonIgnore]
        public virtual User Teacher { get; set; }
        //[JsonIgnore]
        public virtual List<Group> Groups { get; set; } = new();
    }
}
