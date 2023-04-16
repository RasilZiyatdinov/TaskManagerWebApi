using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManagerWebApi.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Subject : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("TeacherId")]
        public User Teacher { get; set; } = new User();

        /// <summary>
        /// 
        /// </summary>
        public List<Group> Groups { get; set; } = new();
    }
}
