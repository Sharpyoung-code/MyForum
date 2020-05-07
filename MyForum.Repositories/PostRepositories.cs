using MyForum.Data;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyForum.Data.Models;
using System.Data.Entity.Include;

namespace MyForum.Repositories
{
    public class PostRepositories : IPost, IDisposable
    {
        private readonly ApplicationDbContext _db;
       // private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public PostRepositories(ApplicationDbContext db)
        {
            this._db = db;
        }
        public async Task Add(Post post)
        {
            try
            {
                _db.Posts.Add(post);
                await _db.SaveChangesAsync();
            }
            catch(Exception exception)
            {

            }
            
        }

        public async Task AddPostReply(PostReply reply)
        {
            _db.PostReplies.Add(reply);
            await _db.SaveChangesAsync();
        }

        public Task Create(Post forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int postId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return _db.Posts
                 .Include(post => post.User)
                 .Include(post => post.Replies).ThenInclude(reply => reply.User)
                 .Include(post => post.Forum);
                
        }

        public Post GetById(int id)
        {
           return _db.Posts.Where(post => post.Id == id)
                .Include(post => post.User)
                .Include(post => post.Replies).ThenInclude(reply => reply.User)
                .Include(post => post.Forum)
                .First();
        }
        public PostReply GetByReplyId(int id)
        {
            return _db.PostReplies.Where(reply => reply.Id == id).First();
        }

        public IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery) ? forum.Posts :
                forum.Posts
                .Where(post => post.Title.Contains(searchQuery) || post.Content.Contains(searchQuery));
        }
        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            return GetAll().Where(post => post.Title.Contains(searchQuery)
                    || post.Content.Contains(searchQuery));
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            var posts = _db.Forums.Where(forum => forum.Id == id).First()
                .Posts;
            return posts;
        }
        public IEnumerable<Post> GetLatestPosts(int v)
        {
            return GetAll().OrderByDescending(post => post.Created).Take(v);
        }

        public async Task UpdatePostContent(Post post)
        {
            try
            {
                
                _db.Entry(post).State = System.Data.Entity.EntityState.Modified;
                await _db.SaveChangesAsync();
                
            }
            catch(Exception exception)
            {

            }
            
        }

        public async Task UpdateReplyContent(PostReply reply)
        {
            try
            {

                _db.Entry(reply).State = System.Data.Entity.EntityState.Modified;
                await _db.SaveChangesAsync();

            }
            catch (Exception exception)
            {

            }
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
