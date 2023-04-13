using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerWebApi.Entities;

namespace TaskManagerApi.Entities
{
    public class User : IdentityUser<int> 
    {
        public string FullName { get; set; }
        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group? Group { get; set; }
        public virtual List<Project>? Projects { get; set; } = new();
        public virtual List<Project>? ManagedProjects { get; set; } = new();
        public virtual List<Subject>? Subjects { get; set; } = new();
        public virtual List<Request>? Requests { get; set; } = new();
        public virtual List<TaskEntity>? TaskEntities { get; set; } = new();
        public virtual List<StudentTask>? StudentTasks { get; set; } = new();
        public virtual List<Comment>? Comments { get; set; } = new();
        public virtual List<Mark>? Marks { get; set; } = new();
    }
}
