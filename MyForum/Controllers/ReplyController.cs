using Microsoft.AspNet.Identity;
using MyForum.Data;
using MyForum.Data.Models;
using MyForum.Models;
using MyForum.Models.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyForum.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IPost _postRepositories;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _applicationUserRepositories;
        public ReplyController(IPost postRepositories, UserManager<ApplicationUser> userManger, IApplicationUser applicationUserRepositories)
        {
            _postRepositories = postRepositories;
            _userManager = userManger;
            _applicationUserRepositories = applicationUserRepositories;
        }
        // GET: Reply
        public async Task<ActionResult> Create(int id)
        {
            var post = _postRepositories.GetById(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyModel
            {
                PostContent = post.Content,
                PostId = post.Id,
                PostTitle = post.Title,
                AuthorName = user.UserName,
                AuthorId = user.Id,
                AuthorRating = user.Rating,
                IsAuthorAdmin = User.IsInRole("Admin"),
                ForumName = post.Forum.Title,
                ForumId = post.Forum.Id,

                DateCreated = DateTime.Now
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddReply(PostReplyModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = _userManager.FindByIdAsync(userId).Result;
            var reply = BuildReply(model, user);

            await _postRepositories.AddPostReply(reply);
            await _applicationUserRepositories.IncrementUserRating(user.Id, typeof(PostReply));
            return RedirectToAction("Index", "Post", new { id = model.PostId});
        }

        private PostReply BuildReply(PostReplyModel model, ApplicationUser user)
        {
            var post = _postRepositories.GetById(model.PostId);
            return new PostReply
            {
                Content = model.ReplyContent,
                Created = DateTime.Now,
                User_Id = user.Id,
                Post_Id = post.Id
            };
        }
    }
}