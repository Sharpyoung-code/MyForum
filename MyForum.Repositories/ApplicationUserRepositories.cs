using MyForum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyForum.Models;
using MyForum.Data.Models;

namespace MyForum.Repositories
{
    public class ApplicationUserRepositories : IApplicationUser
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepositories(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<ApplicationUser> GetAll()
        {
            return _db.Users;
        }

        public ApplicationUser GetById(string id)
        {
            return GetAll().FirstOrDefault(user => user.Id == id);
        }

        public async Task IncrementUserRating(string userId, Type type)
        {
            var user = GetById(userId);
            var newRating = CalculateUserRating(type, user.Rating);
            user.Rating = newRating;
            await _db.SaveChangesAsync();
        }

        private int CalculateUserRating(Type type, int userRating)
        {
            var inc = 0;
            if (type == typeof(Post))
                inc = 2;
            if (type == typeof(PostReply))
                inc = 5;
            return userRating + inc;
        }

        public async Task SetProfileImage(string id, Uri eri)
        {
            var user = GetById(id);
            //_db.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
