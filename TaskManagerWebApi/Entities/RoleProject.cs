using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleProject : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("ProjectId")]
        public Project Project { get; set; } = new Project();
    }
}
