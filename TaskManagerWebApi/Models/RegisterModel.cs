using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisterModel
    {
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
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public int? GroupId { get; set; }
    }
}
