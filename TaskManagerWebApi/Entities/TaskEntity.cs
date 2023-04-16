using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskEntity : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>

        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set;}

        /// <summary>
        /// 
        /// </summary>

        [Column(TypeName = "Date")]
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>

        [Column(TypeName = "text")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public int PriorityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("PriorityId")]
        public virtual Priority Priority { get; set; } = new Priority();

        /// <summary>
        /// 
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; } = new Status();

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; } = new Project();

        /// <summary>
        /// 
        /// </summary>
        public int TeamRoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("TeamRoleId")]
        public RoleProject TeamRole { get; set; } = new RoleProject();

        /// <summary>
        /// 
        /// </summary>
        public List<User> Students { get; set; } = new List<User>();

        /// <summary>
        /// 
        /// </summary>
        public List<StudentTask> StudentTasks { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<Comment> Comments { get; set; } = new();

    }
}
