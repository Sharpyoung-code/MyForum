using MyForum.Data;
using MyForum.Models.Home;
using MyForum.Models.Post;
using System.Linq;
using System.Web.Mvc;
using MyForum.Data.Models;
using MyForum.Models;
using System;

namespace MyForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPost _postRepositories;
        public HomeController(IPost postRepositories)
        {
            _postRepositories = postRepositories;
        }
        public ActionResult Index()
        {
            var model = BuildHomeIndexModel();
            return View(model);
        }

        private HomeIndexModel BuildHomeIndexModel()
        {
            var latestPosts = _postRepositories.GetLatestPosts(20);
            var posts = latestPosts.Select(post => new PostListingModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorName = post.User.UserName,
                AuthorRating = post.User.Rating,
                AuthorId = post.User.Id,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = GetForumListingForPost(post)

            });
            return new HomeIndexModel
            {
                LatestPosts = posts,
                SearchQuery = ""
            };
        }

        private ForumListingModel GetForumListingForPost(Post post)
        {
            var forum = post.Forum;
            return new ForumListingModel
            {
                Name = forum.Title,
                Id = forum.Id
            };
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}