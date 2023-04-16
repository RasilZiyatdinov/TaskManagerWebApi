using System.Text.Json.Serialization;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class TeamRole : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
