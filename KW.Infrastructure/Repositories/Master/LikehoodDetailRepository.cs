using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class LikehoodDetailRepository : ILikehoodDetailRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public LikehoodDetailRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        public bool IsExist(string definisiLikehood)
        {
            var results = _databaseContext.LikehoodDetails.Where(x => x.DefinisiLikehood.ToLower() == definisiLikehood.ToLower() && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, string definisiLikehood)
        {
            var results = _databaseContext.LikehoodDetails.Where(x => x.DefinisiLikehood.ToLower() == definisiLikehood.ToLower() && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public LikehoodDetail Get(int id)
        {
            return _databaseContext.LikehoodDetails.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<LikehoodDetail> GetAll()
        {
            return _databaseContext.LikehoodDetails.Where(x => x.IsDelete == false).ToList();
        }

        public IEnumerable<LikehoodDetail> GetByLikehoodId(int likehoodId)
        {
            return _databaseContext.LikehoodDetails.Where(x => x.LikehoodId == likehoodId && x.IsDelete == false).ToList();
        }

        public void Insert(LikehoodDetail model)
        {
            _databaseContext.LikehoodDetails.Add(model);
        }

        public void Update(LikehoodDetail model)
        {
            _databaseContext.LikehoodDetails.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }

        public void Insert(IList<LikehoodDetail> collections)
        {
            throw new NotImplementedException();
        }
    }
}
