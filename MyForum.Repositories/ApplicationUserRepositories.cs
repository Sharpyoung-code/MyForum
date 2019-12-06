using MyForum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyForum.Models;

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

        public Task IncrementRating(string id, Type type)
        {
            throw new NotImplementedException();
        }

        public async Task SetProfileImage(string id, Uri eri)
        {
            var user = GetById(id);
            //_db.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
