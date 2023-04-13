using Microsoft.EntityFrameworkCore;
using TaskManagerApi.DAL;
using TaskManagerApi.Entities;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerApi.Services
{
    public class GroupService : IGroupService
    {
        private readonly ApplicationDbContext dbContext;

        public GroupService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await dbContext.Group.ToListAsync();
        }

        public async Task<Group> AddGroupAsync(Group g)
        {
            var gr = await dbContext.Group.AddAsync(g);
            await dbContext.SaveChangesAsync();
            return gr.Entity;
        }

        public async Task<Group> UpdateGroupAsync(Group g)
        {
            var updateResult = dbContext.Group.Update(g);
            await dbContext.SaveChangesAsync();
            return updateResult.Entity;
        }

        public async Task<bool> DeleteGroupAsync(long id)
        {
            var group = await dbContext.Group.FirstOrDefaultAsync(x => x.Id == id);
            var deleteResult = dbContext.Remove(group);
            await dbContext.SaveChangesAsync();
            return deleteResult != null ? true : false;
        }
    }
}
