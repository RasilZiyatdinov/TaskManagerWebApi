using System.ComponentModel.DataAnnotations;


namespace TaskManagerWebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
