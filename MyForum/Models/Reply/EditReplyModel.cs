using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyForum.Models.Reply
{
    public class EditReplyModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "New Content")]
        [DataType(DataType.MultilineText)]
        public string NewReplyContent { get; set; }
        public string UserId { get; set; }
        public int? PostId { get; set; }
        public string ForumName { get; set; }
        public string PostContent { get; set; }
        public string PostTitle { get; set; }
        public DateTime DateCreated { get; set; }
    }
}