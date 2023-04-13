using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerApi.Entities;

namespace TaskManagerWebApi.Models
{
    public class TaskModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Description { get; set; }

        public int StatusId { get; set; }

        public int PriorityId { get; set; }

        public int ProjectId { get; set; }

        public int TeamRoleId { get; set; }

        public List<int> StudentIds { get; set; } = new();
    }
}
