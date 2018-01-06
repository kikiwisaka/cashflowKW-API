using KW.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseContext _databaseContext;
        public UserRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public User Get(int id)
        {
            return _databaseContext.Users.SingleOrDefault(x => x.Id.Equals(id));
        }

        public IEnumerable<User> GetAll()
        {
            var results = _databaseContext.Users.AsQueryable();
            return results;
        }

        public User Find(string username)
        {
            return _databaseContext.Users
                .Where(p => p.UserName.Equals(username))
                .SingleOrDefault();
        }


        public bool EmailExist(string username)
        {
            throw new NotImplementedException();
        }

        public bool isExist(string username)
        {
            var result = this.Find(username);
            if (result != null)
                return true;
            else
                return false;
        }
    }
}
