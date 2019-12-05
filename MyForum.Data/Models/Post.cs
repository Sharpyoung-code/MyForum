using MyForum.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        
        public string User_Id { get; set; }
       
        public int? Forum_Id { get; set; }
        [ForeignKey("User_Id")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("Forum_Id")]
        public virtual Forum Forum { get; set; }

        public virtual ICollection<PostReply> Replies { get; set; }
    }
}
