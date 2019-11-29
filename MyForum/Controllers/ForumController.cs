using MyForum.Data;
using MyForum.Models;
using MyForum.Models.Forum;
using MyForum.Models.Post;
using System.Linq;
using System.Web.Mvc;

namespace MyForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumRepositories;
        private readonly IPost _postRepositories;
        public ForumController(IForum forumRepositories)
        {
            _forumRepositories = forumRepositories;
        }
        // GET: Forum
        public ActionResult Index()
        {
            var forums = _forumRepositories.GetAll().Select( forum => new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description
            });

            var model = new ForumIndexViewModel
            {
                ForumList = forums
            };
            return View(model);
        }
        public ActionResult Topic(int id)
        {
            var forum = _forumRepositories.GetById(id);
            var post = _postRepositories.GetFilteredPosts(id);

            var postListing = new PostListingModel
            {

            };
        }
    }
}