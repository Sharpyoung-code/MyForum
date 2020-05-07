using MyForum.Data;
using System;
using System.Data.Entity.Include;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyForum.Data.Models;
using MyForum.Models;

namespace MyForum.Repositories
{
    public class ForumRepositories : IForum, IDisposable
    {
        private readonly ApplicationDbContext _db;
        public ForumRepositories(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(Forum forum)
        {
            _db.Forums.Add(forum);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int forumId)
        {
            var forum = GetById(forumId);
            _db.Forums.Remove(forum);
            
            await _db.SaveChangesAsync();
         
        }

        public IEnumerable<Forum> GetAll()
        {
            return _db.Forums
                .Include(forum => forum.Posts);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers(int id)
        {
            var posts = GetById(id).Posts;
            if(posts != null || !posts.Any())
            {
                var postUsers = posts.Select(p => p.User);
                var replyUsers = posts.SelectMany(p => p.Replies).Select(r => r.User);
                return postUsers.Union(replyUsers).Distinct();
            }
            return new List<ApplicationUser>();
        }

        public Forum GetById(int id)
        {
            var forum = _db.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts).ThenInclude(p => p.User)
                .Include(f => f.Posts).ThenInclude(p => p.Replies).ThenInclude(r => r.User)
                .FirstOrDefault();

            return forum;
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
