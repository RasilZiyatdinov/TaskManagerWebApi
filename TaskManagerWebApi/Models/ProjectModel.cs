using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectModel
    {

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public int MembersNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PlannedExpirationDate { get; set; }
    }
}
