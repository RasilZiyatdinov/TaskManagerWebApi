using System.Text.Json.Serialization;
using TaskManagerWebApi.Entities;

namespace TaskManagerApi.Entities
{
    public class TeamRole : BaseEntity
    {
        public string Name { get; set; }
        //[JsonIgnore]
        //public virtual List<Project> Projects { get; set; } = new();

        //public virtual List<StudentTask>? StudentTasks { get; set; } = new();

    }
}
