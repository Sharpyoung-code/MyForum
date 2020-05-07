using MyForum.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Data.Models
{
    public class PostReply
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public int? Post_Id { get; set; }
        [Required]
        public string User_Id { get; set; }
        [ForeignKey("User_Id")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("Post_Id")]
        [CascadeDelete]
        public virtual Post Post { get; set; }
    }
}
