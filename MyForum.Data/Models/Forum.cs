using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Data.Models
{
    public class Forum
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

        [CascadeDelete]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
