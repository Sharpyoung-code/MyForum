using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;

namespace MyForum.Models.Post
{
    public class EditPostModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "New Title")]
        [DataType(DataType.Text)]
        public string NewPostTitle { get; set; }
        [Display(Name = "New Content")]
        [DataType(DataType.MultilineText)]
        public string NewPostContent { get; set; }
        public string UserId { get; set; }
        public int? ForumId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}