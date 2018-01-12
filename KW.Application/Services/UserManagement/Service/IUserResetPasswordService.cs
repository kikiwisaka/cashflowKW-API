using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application
{
    public interface IUserResetPasswordService
    {
        void ResetPassword(string userName, string url);
    }
}
