using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Models.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskDTO
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
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public Status Status { get; set; } = new Status();


        /// <summary>
        /// 
        /// </summary>
        public Priority Priority { get; set; } = new Priority();


        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TeamRole { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public List<UserDTO> Students { get; set; } = new List<UserDTO>();
    }
}
