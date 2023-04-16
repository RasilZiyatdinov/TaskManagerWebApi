
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Models.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class SubjectDTO
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
        public UserDTO? Teacher { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Group> Groups { get; set; } = new();
    }
}
