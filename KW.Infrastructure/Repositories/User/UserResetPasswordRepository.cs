using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Infrastructure.Repositories
{
    public class UserResetPasswordRepository : IUserResetPasswordRepository
    {
        private readonly IDatabaseContext _databaseContext;
        public UserResetPasswordRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public UserResetPassword Get(string requestToken)
        {
            return _databaseContext.UserResetPasswords.SingleOrDefault(p => p.RequestToken.Equals(requestToken));
        }

        public void Insert(UserResetPassword resetData)
        {
            _databaseContext.UserResetPasswords.Add(resetData);
        }

        public bool IsExist(string requestToken)
        {
            var results = _databaseContext.UserResetPasswords.Where(p => p.RequestToken.Equals(requestToken)).ToList();
            if (results.Count > 0)
                return true;
            else
                return false;
        }
    }
}
