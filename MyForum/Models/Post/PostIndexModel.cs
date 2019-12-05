using MyForum.Models.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyForum.Models.Post
{
    public class PostIndexModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorId { get; set; }
        public DateTime DateCreated { get; set; }
        public string PostContent { get; set; }
        public string ForumName { get; set; }
        public int ForumId { get; set; }
        public bool IsAdmin { get; set; }

        public IEnumerable<PostReplyModel> Replies { get; set; }
    }
}