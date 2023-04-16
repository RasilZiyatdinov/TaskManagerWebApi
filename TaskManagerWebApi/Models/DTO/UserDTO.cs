using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Models.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public RoleDTO Role { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Group? Group { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        public UserDTO(User user, RoleDTO role)
        {
            Id = user.Id;
            FullName = user.FullName;
            Email = user.Email;
            Group = user.Group;
            Role = role;
        }
    }
}
