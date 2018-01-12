using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IUserResetPasswordRepository
    {
        UserResetPassword Get(string requestToken);
        bool IsExist(string requestToken);
        void Insert(UserResetPassword resetData);
    }
}
