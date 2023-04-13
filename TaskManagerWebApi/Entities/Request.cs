using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerApi.Entities;

namespace TaskManagerWebApi.Entities
{
    public class Request
    {
        [JsonIgnore]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual User? Student { get; set; }

        [JsonIgnore]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project? Project { get; set; }

        public virtual RequestStatus Status { get; set; }
        public virtual IdentityRole<int> Role { get; set; }

        public DateTime? Date { get; set; }
    }
}
