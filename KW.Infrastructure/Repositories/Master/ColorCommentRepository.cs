using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class ColorCommentRepository : IColorCommentRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public ColorCommentRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ColorComment Get(int? id)
        {
            return _databaseContext.ColorComments.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<ColorComment> GetAll()
        {
            //return _databaseContext.ColorComments.AsQueryable();
            return _databaseContext.ColorComments.Where(x => x.IsDelete == false).ToList();

        }

        public void Insert(ColorComment model)
        {
            _databaseContext.ColorComments.Add(model);
        }

        public bool IsExist(int id, string warna)
        {
            var results = _databaseContext.ColorComments.Where(x => x.Warna.ToLower() == warna.ToLower() && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(string warna)
        {
            var results = _databaseContext.ColorComments.Where(x => x.Warna.ToLower() == warna.ToLower() && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(ColorComment model)
        {
            _databaseContext.ColorComments.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
