using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManagerApi.Entities;

namespace TaskManagerApi.Models
{
    public class SubjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public List<int> GroupsIds { get; set; } = new();
    }
}
