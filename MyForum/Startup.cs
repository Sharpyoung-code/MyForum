using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using MyForum.Data;
using MyForum.Models;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(MyForum.Startup))]
namespace MyForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRole();
        }
        private void CreateUserAndRole()
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "ForumAdmin";
                user.Email = "admin@techforum.com";
                user.MemberSince = DateTime.Now;
                user.IsActive = true;
                user.PhoneNumber = "08134099842";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.PhoneNumberConfirmed = true;
                string userPassword = "2019@Techforum";

                var createUser = UserManager.Create(user, userPassword);

                if (createUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
