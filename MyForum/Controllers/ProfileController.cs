using Microsoft.AspNet.Identity;
using MyForum.Data;
using MyForum.Models;
using MyForum.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyForum.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userRepositories;
        private readonly IUpload _uploadRepositories;
        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userRepositories, IUpload uploadRepositories)
        {
            _userManager = userManager;
            _userRepositories = userRepositories;
            _uploadRepositories = uploadRepositories;
        }
        [Authorize]
        // GET: Profile
        public ActionResult Details(string id)
        {
            var user = _userRepositories.GetById(id);
            var userRole = _userManager.GetRolesAsync(user.Id).Result.Contains("Admin");
            var model = new ProfileModel()
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                MemberSince = user.MemberSince,
                UserRating = user.Rating.ToString(),
                IsAdmin = userRole
            };
            return View(model); 
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var profiles = _userRepositories.GetAll()
                .OrderByDescending(user => user.Rating)
                .Select(u => new ProfileModel
                {
                    Email = u.Email,
                    UserName = u.UserName,
                    UserRating = u.Rating.ToString(),
                    MemberSince = u.MemberSince,

                });
            var model = new ProfileListingModel
            {
                Profiles = profiles
            };
            return View(model);
        }
    }
}