using System.ComponentModel.DataAnnotations;

namespace TaskManager.Data
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; } = string.Empty;
        public int? GroupId { get; set; }
    }
}
