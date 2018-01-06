using KW.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IDatabaseContext _databaseContext;
        public UserRoleRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<UserRole> GetAll()
        {
            var results = _databaseContext.UserRoles.AsQueryable();
            return results;
        }

        //public UserRole Find(string username)
        //{
        //    return _databaseContext.UserRoles
        //        .Where(p => p.User.UserName.Equals(username))
        //        .SingleOrDefault();
        //}

        public UserRole GetByUserId(int userId)
        {
            return _databaseContext.UserRoles
                .Where(p => p.UserId.Equals(userId))
                .SingleOrDefault();
        }

        public UserRole GetByRoleId(int roleId)
        {
            return _databaseContext.UserRoles
                .Where(p => p.RoleId.Equals(roleId))
                .SingleOrDefault();
        }
    }
}
