using TaskManagerApi.Entities;

namespace TaskManagerApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Group? Group { get; set; }

        public UserModel(User? user, string role = "")
        {
            if (user != null)
            {
                Id = user.Id;
                FullName = user.FullName;
                Email = user.Email;
                Group = user.Group;
                Role = role;
            }
        }
    }
}
