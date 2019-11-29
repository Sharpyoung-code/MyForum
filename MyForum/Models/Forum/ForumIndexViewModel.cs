using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyForum.Models.Forum
{
    public class ForumIndexViewModel
    {
        public IEnumerable<ForumListingModel> ForumList { get; set; }
    }
}