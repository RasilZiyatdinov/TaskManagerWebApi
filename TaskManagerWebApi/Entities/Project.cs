using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using TaskManagerWebApi.Entities;
using Castle.Components.DictionaryAdapter;

namespace TaskManagerWebApi.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Project: BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        public int MembersNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; } = new Subject();

        /// <summary>
        /// 
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("StatusId")]
        public Status Status { get; set; } = new Status();

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ManagerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("ManagerId")]
        public User? Manager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<RoleProject> TeamRoles { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<User> RequestedParticipants { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<Request> Requests { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<Mark> Marks { get; set; } = new();

    }
}
