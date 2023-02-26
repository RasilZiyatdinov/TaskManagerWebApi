using TaskManagerApi.Entities;

namespace TaskManagerApi.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task<Group> AddGroupAsync(Group g);
        Task<Group> UpdateGroupAsync(Group g);
        Task<bool> DeleteGroupAsync(long id);
    }
}
