using MyForum.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyForum.Models.Home
{
    public class HomeIndexModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<PostListingModel> LatestPosts { get; set; }
    }
}