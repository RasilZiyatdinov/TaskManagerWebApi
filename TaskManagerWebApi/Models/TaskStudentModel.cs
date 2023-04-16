using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskStudentModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AddHoursNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
    }
}
