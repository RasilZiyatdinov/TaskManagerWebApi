namespace TaskManagerWebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangePasswordModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; } = String.Empty;

    }
}
