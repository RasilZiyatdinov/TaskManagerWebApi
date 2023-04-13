using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerApi.Entities;

namespace TaskManagerWebApi.Entities
{
    public class RoleProject : BaseEntity
    {
        public string Name {  get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        //public virtual List<StudentTask>? StudentTasks { get; set; } = new();
    }
}
