using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerApi.Entities;

namespace TaskManagerWebApi.Entities
{
    public class Mark
    {
        [JsonIgnore]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [JsonIgnore]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project? Project { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        public int Grade { get; set; }
    }
}
