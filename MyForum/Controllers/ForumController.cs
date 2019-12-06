using MyForum.Data;
using MyForum.Models;
using MyForum.Models.Forum;
using MyForum.Models.Post;
using System.Linq;
using System.Web.Mvc;
using MyForum.Data.Models;
using System;
using MyForum.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumRepositories;
        private readonly IPost _postRepositories;
        public ForumController(IForum forumRepositories, IPost postRepositories)
        {
            _forumRepositories = forumRepositories;
            _postRepositories = postRepositories;
        }
        /*public ForumController()
        {
            this._forumRepositories = _forumRepositories;
        }*/
        // GET: Forum
        public ActionResult Index()
        {
            var forums = _forumRepositories.GetAll().Select(forum => new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                Created = forum.Created.ToString(),
                postsCount = forum.Posts.Count
            });

            var model = new ForumIndexViewModel
            {
                ForumList = forums
            };
            return View(model);
        }
        public ActionResult Topic(int id, string searchQuery)
        {
            var forum = _forumRepositories.GetById(id);
            var posts = new List<Post>();

            posts = _postRepositories.GetFilteredPosts(forum, searchQuery).ToList();

            var postListing = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorName = post.User.UserName,
                AuthorId = post.User.Id,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                AuthorRating = post.User.Rating,
                Forum = BuildForumListing(post)
            });
            var model = new ForumTopicViewModel
            {
                Posts = postListing,
                Forum = BuildForumListing(forum)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", new { id, searchQuery });
        }
        public ActionResult Create()
        {
            var model = new AddNewForumModel();
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddForum(AddNewForumModel model)
        {
            var forum = new Forum
            {
                Title = model.Title,
                Description = model.Description,
                Created = DateTime.Now
            };
            await _forumRepositories.Create(forum);
            return RedirectToAction("Index");
        }
        private ForumListingModel BuildForumListing(Forum forum)
        {

            return new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description
            };
        }
        private ForumListingModel BuildForumListing(Post posts)
        {
            var forum = posts.Forum;
            return BuildForumListing(forum);
        }
    }
}