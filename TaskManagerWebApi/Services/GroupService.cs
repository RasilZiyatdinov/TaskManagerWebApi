using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.DAL;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerWebApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupService : IGroupService
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dbContext"></param>
        public GroupService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await dbContext.Group.ToListAsync();
        }


        //public async Task<IEnumerable<Group>> GetGroupsBySubjAsync(int subjectId)
        //{
        //    return await dbContext.Group.Where()ToListAsync();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public async Task<Group> AddGroupAsync(Group g)
        {
            var gr = await dbContext.Group.AddAsync(g);
            await dbContext.SaveChangesAsync();
            return gr.Entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public async Task<Group> UpdateGroupAsync(Group g)
        {
            var updateResult = dbContext.Group.Update(g);
            await dbContext.SaveChangesAsync();
            return updateResult.Entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteGroupAsync(long id)
        {
            var group = await dbContext.Group.FirstAsync(x => x.Id == id);
            var deleteResult = dbContext.Remove(group);
            await dbContext.SaveChangesAsync();
            return deleteResult != null ? true : false;
        }
    }
}
