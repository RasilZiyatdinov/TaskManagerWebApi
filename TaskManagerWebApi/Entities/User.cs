using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerWebApi.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class User : IdentityUser<int> 
    {
        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; set; } = string.Empty;
        
        /// <summary>
        /// 
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("GroupId")]
        public virtual Group? Group { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Project> Projects { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<Project> ManagedProjects { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<Subject> Subjects { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<Request> Requests { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<TaskEntity> TaskEntities { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<StudentTask> StudentTasks { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<Comment> Comments { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<Mark> Marks { get; set; } = new();
    }
}
