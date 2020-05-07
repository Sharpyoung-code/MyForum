using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyForum.Models
{
    public class ForumListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Created { get; set; }
        public int postsCount { get; set; }
        public int ActiveUsers { get; set; }
    }
}