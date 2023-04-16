using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Mark
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("UserId")]
        public User? User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Grade { get; set; }
    }
}
