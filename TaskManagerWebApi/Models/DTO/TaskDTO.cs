using TaskManagerApi.Entities;
using TaskManagerApi.Models;

namespace TaskManagerWebApi.Models.DTO
{
    public class TaskDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public Priority Priority { get; set; }

        public int ProjectId { get; set; }

        public string TeamRole { get; set; }

        public IEnumerable<UserModel> Students { get; set; }
    }
}
