using Microsoft.AspNet.Identity;
using MyForum.Data;
using MyForum.Data.Models;
using MyForum.Models;
using MyForum.Models.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyForum.Controllers
{
    [Authorize]
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
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reply = _postRepositories.GetByReplyId(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            var model = new EditReplyModel
            {
                Id = reply.Id,
                NewReplyContent = reply.Content,
                PostId = reply.Post_Id,
                UserId = reply.User_Id,
                DateCreated = reply.Created,
                ForumName = reply.Post.Forum.Title,
                PostContent = reply.Post.Content,
                PostTitle = reply.Post.Title
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditReply([Bind(Include = "Id, NewReplyContent, PostId, UserId, DateCreated")] EditReplyModel model)
        {
            if (ModelState.IsValid)
            {
                var reply = BuildEditPostModel(model);
                await _postRepositories.UpdateReplyContent(reply);
                return RedirectToAction("Index", "Post", new { id = model.PostId });
            }
            return View(model);
        }

        private PostReply BuildEditPostModel(EditReplyModel model)
        {
            return new PostReply
            {
                Id = model.Id,
                Content = model.NewReplyContent,
                Post_Id = model.PostId,
                User_Id = model.UserId,
                Created = model.DateCreated
            };
        }
    }
}