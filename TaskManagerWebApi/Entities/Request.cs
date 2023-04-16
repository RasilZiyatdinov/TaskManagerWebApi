using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Request
    {
        /// <summary>
        /// 
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("StudentId")]
        public User Student { get; set; } = new User();

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("ProjectId")]
        public Project Project { get; set; } = new Project();

        /// <summary>
        /// 
        /// </summary>
        public RequestStatus Status { get; set; } = new RequestStatus();

        /// <summary>
        /// 
        /// </summary>
        public IdentityRole<int> Role { get; set; } = new IdentityRole<int>();

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; }
    }
}
