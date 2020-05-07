using MyForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Data
{
    public interface IPost : IDisposable
    {
        Post GetById(int id);
        PostReply GetByReplyId(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts( Forum forum, string searchQuery);
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        IEnumerable<Post> GetLatestPosts(int v);

        Task Create(Post forum);
        Task Delete(int postId);
        Task UpdateReplyContent(PostReply reply);
        Task UpdatePostContent(Post post);
        Task AddPostReply(PostReply reply);
        Task Add(Post post);
        
        
    }
}
