using MyForum.Models.Post;
using System.Collections.Generic;

namespace MyForum.Models.Search
{
    public class SearchViewModel
    {
        public IEnumerable<PostListingModel> Posts { get; set; }
        public string SearchQuery { get; set; }
        public bool EmptySearchResults { get; set; }
    }
}