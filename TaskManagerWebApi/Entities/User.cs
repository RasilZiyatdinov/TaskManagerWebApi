using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManagerApi.Entities
{
    public class User : IdentityUser<int> 
    {
        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group? Group { get; set; }
        public List<Project>? Projects { get; set; } = new();
        public List<Project>? ManagedProjects { get; set; } = new();
        public List<Subject>? Subjects { get; set; } = new();
    }
}
