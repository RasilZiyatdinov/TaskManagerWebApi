using TaskManagerApi.Models;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Models.DTO
{
    public class RequestDTO
    {
        public UserModel Student { get; set; }

        public string Status { get; set; }

        public string Role { get; set; }
    }
}
