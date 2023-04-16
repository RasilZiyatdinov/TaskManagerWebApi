using System.Text.Json.Serialization;

namespace TaskManagerWebApi.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Group : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        public virtual List<User> Students { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public virtual List<Subject> Subjects { get; set; } = new();
    }
}
