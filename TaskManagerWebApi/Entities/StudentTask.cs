
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class StudentTask
    {
        /// <summary>
        /// 
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("StudentId")]
        public virtual User Student { get; set; } = new User();

        /// <summary>
        /// 
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("TaskId")]
        public virtual TaskEntity Task { get; set; } = new TaskEntity();

        /// <summary>
        /// 
        /// </summary>
        public int HoursNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
    }
}
