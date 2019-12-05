using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyForum.Models.Reply
{
    public class PostReplyModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorId { get; set; }
        public DateTime DateCreated { get; set; }
        public string ReplyContent { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public int PostId { get; set; }
    }
}