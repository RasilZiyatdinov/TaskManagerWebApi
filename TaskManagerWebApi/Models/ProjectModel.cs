using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerApi.Entities;

namespace TaskManagerWebApi.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MembersNum { get; set; }
        public int SubjectId { get; set; }
        public DateTime PlannedExpirationDate { get; set; }

        //public int TeacherId { get; set; }
        ////public int Manager { get; set; }
        //public List<int> Participants { get; set; }
    }
}
