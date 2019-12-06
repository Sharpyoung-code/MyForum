using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyForum.Models.Profile
{
    public class ProfileModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserRating { get; set; }
        public string ProfileImageurl { get; set; }
        public bool IsAdmin { get; set; }

        public DateTime MemberSince { get; set; }
        //public IFormFile ImageUpload { get; set; }
    }
}