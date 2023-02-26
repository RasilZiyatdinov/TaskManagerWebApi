using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerApi.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string Name { get; set; }

        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set;}
        public int Hours { get; set;}

        [Column(TypeName = "text")]
        public string Description { get; set;}

        public int PriorityId { get; set; }
        [ForeignKey("PriorityId")]
        public Priority Priority { get; set;}

        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
    }
}
