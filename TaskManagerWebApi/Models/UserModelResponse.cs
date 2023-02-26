using TaskManagerApi.Entities;

namespace TaskManagerApi.Models
{
    public class UserModelResponse
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Group { get; set; }

        public UserModelResponse(User user, string role)
        {
            Id = user.Id;
            FullName = user.UserName;
            Email = user.Email;
            Group = user.Group?.Name;
            Role = role;
        }
    }
}
