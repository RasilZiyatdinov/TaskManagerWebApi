using TaskManagerWebApi.Models;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Models.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestDTO
    {

        /// <summary>
        /// 
        /// </summary>
        public UserDTO? Student { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; }
    }
}
