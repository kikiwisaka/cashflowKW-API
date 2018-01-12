using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IUserRoleRepository
    {
        IEnumerable<UserRole> GetAll();
        UserRole GetByUserId(int userId);
        UserRole GetByRoleId(int roleId);
    }
}
