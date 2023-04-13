
using TaskManagerApi.Entities;
using TaskManagerApi.Models;

namespace TaskManagerWebApi.Models.DTO
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserModel Teacher { get; set; }
        public List<Group> Groups { get; set; } = new();
    }
}
