using TaskManagerApi.Entities;
using TaskManagerApi.Models;

namespace TaskManagerWebApi.Models.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MembersNum { get; set; }

        public DateTime ExpirationDate { get; set; }

        public SubjectDTO Subject { get; set; }

        public string Status { get; set; }

        public UserModel Manager { get; set; }

        public IEnumerable<RequestDTO> Requests { get; set; } 

        public List<string> TeamRoles { get; set; } = new();

    }
}
