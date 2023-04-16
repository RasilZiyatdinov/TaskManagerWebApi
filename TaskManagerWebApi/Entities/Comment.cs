using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("UserId")]
        public User User { get; set; } = new User();

        /// <summary>
        /// 
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("TaskId")]
        public TaskEntity Task { get; set; } = new TaskEntity();

        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime UpdateDate { get; set; }
    }
}
