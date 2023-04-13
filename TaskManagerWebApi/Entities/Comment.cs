using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerApi.Entities;

namespace TaskManagerWebApi.Entities
{
    public class Comment
    {
        [JsonIgnore]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [JsonIgnore]
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual TaskEntity? Task { get; set; }

        public string Body { get; set; }

        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime UpdateDate { get; set; }
    }
}
