using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerApi.Entities;

namespace TaskManagerApi.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MembersNum { get; set; }
        public int Subject { get; set; }
        public int Teacher { get; set; }
        //public int Manager { get; set; }
        public List<int> Participants { get; set; }
    }
}
