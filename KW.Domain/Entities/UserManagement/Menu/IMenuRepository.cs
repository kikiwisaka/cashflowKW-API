using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IMenuRepository
    {
        IQueryable<Menu> GetMenu();
        Menu Get(int Id);
        IList<Menu> GetChild(int parentId);
    }
}
