using Microsoft.AspNet.Identity;
using System.Security.Claims;
using MyForum.Data;
using MyForum.Data.Models;
using MyForum.Models;
using MyForum.Models.Post;
using MyForum.Models.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MyForum.Repositories;

namespace MyForum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postRepositories;
        private readonly IForum _forumRepositories;
        private readonly IApplicationUser _applicationUserRepositories;

        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(IPost postRepositories, IForum forumRepositories, UserManager<ApplicationUser> userManager, IApplicationUser applicationUserRepositories)
        {
            _postRepositories =  postRepositories;
            _forumRepositories = forumRepositories;
            _userManager = userManager;
            _applicationUserRepositories = applicationUserRepositories;
        }
        
        // GET: Post
        public ActionResult Index(int id)
        {
            var posts = _postRepositories.GetById(id);
            var replies = BuildPostReplies(posts.Replies);
            var model = new PostIndexModel
            {
                Id = posts.Id,
                Title = posts.Title,
                AuthorId = posts.User.Id,
                AuthorName = posts.User.UserName,
                AuthorRating = posts.User.Rating,
                DateCreated = posts.Created,
                PostContent = posts.Content,
                Replies = replies,
                ForumId = posts.Forum.Id,
                ForumName = posts.Forum.Title,
                IsAdmin = IsAdmin(posts.User)
                
            };
            
            return View(model);
        }

        private bool IsAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user.Id)
                .Result.Contains("Admin");
        }

        public ActionResult Create (int id)
        {
            var forum = _forumRepositories.GetById(id);
            var model = new NewPostModel
            {
                ForumId = forum.Id,
                ForumName = forum.Title,
                AuthorName = User.Identity.Name
            };
            return View(model);
        }
        [HttpPost]
        public  async Task<ActionResult> AddPost(NewPostModel model)
        {
            var userId = User.Identity.GetUserId();
            var user =  _userManager.FindByIdAsync(userId).Result;
            var post = BuildPost(model, user);

            await _postRepositories.Add(post);
            await _applicationUserRepositories.IncrementUserRating(user.Id, typeof(Post));
            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            var forum = _forumRepositories.GetById(model.ForumId);
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User_Id = user.Id,
                Forum_Id = forum.Id
            };
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorRating = reply.User.Rating,
                DateCreated = reply.Created,
                ReplyContent = reply.Content,
                IsAuthorAdmin = IsAuthorAdmin(reply.User)
            });
        }

        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user.Id)
                .Result.Contains("Admin");
        }
    }
}