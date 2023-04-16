using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Models.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectDTO
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
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SubjectDTO Subject { get; set; } = new SubjectDTO();

        /// <summary>
        /// 
        /// </summary>
        public Status Status { get; set; } = new Status();

        /// <summary>
        /// 
        /// </summary>
        public UserDTO? Manager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<RequestDTO>? Requests { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public List<RoleDTO>? TeamRoles { get; set; }

    }
}
