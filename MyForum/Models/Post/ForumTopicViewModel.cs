using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyForum.Models.Post
{
    public class ForumTopicViewModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<PostListingModel> Posts { get; set; }
    }
}