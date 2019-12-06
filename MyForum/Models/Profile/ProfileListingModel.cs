using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyForum.Models.Profile
{
    public class ProfileListingModel
    {
        public IEnumerable<ProfileModel> Profiles { get; set; }
    }
}