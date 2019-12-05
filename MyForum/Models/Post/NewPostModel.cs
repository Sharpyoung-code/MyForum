using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyForum.Models.Post
{
    public class NewPostModel
    {
        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}