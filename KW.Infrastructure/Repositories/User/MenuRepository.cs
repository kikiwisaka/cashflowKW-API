using KW.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Infrastructure.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly IDatabaseContext _databaseContext;
        public MenuRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Menu Get(int id)
        {
            return _databaseContext.Menus.SingleOrDefault(x => x.Id.Equals(id));
        }

        public IEnumerable<Menu> GetAll()
        {
            var results = _databaseContext.Menus.AsQueryable();
            return results;
        }

        public IList<Menu> GetChild(int parentId)
        {
            return _databaseContext.Menus.Where(x => x.Parent.Id == parentId).ToList();
        }

        public IQueryable<Menu> GetMenu()
        {
            return _databaseContext.Menus.AsQueryable();
        }
    }
}
