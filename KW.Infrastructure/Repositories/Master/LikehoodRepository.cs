using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class LikehoodRepository : ILikehoodRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public LikehoodRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Likehood Get(int id)
        {
            return _databaseContext.Likehoods.SingleOrDefault(x => x.Id == id);
        }

        public Likehood GetDefault()
        {
            return _databaseContext.Likehoods.SingleOrDefault(x => x.Status == true && x.IsDelete == false);
        }

        public IEnumerable<Likehood> GetAll()
        {
            return _databaseContext.Likehoods.Where(x => x.IsDelete == false).ToList();

        }

        public void Insert(Likehood model)
        {
            _databaseContext.Likehoods.Add(model);
        }

        public bool IsExist(int id, string namaLikehood)
        {
            var results = _databaseContext.Likehoods.Where(x => x.NamaLikehood.ToLower() == namaLikehood.ToLower() && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(string namaLikehood)
        {
            var results = _databaseContext.Likehoods.Where(x => x.NamaLikehood.ToLower() == namaLikehood.ToLower() && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(Likehood model)
        {
            _databaseContext.Likehoods.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
