using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class OverAllCommentsRepository : IOverAllCommentsRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public OverAllCommentsRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public OverAllComments Get(int id)
        {
            return _databaseContext.OverAllCommentss.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<OverAllComments> GetAll()
        {
            //return _databaseContext.OverAllCommentss.AsQueryable();
            return _databaseContext.OverAllCommentss.Where(x => x.IsDelete == false).ToList();

        }
        public IEnumerable<OverAllComments> GetByColorId(int colorId)
        {
            return _databaseContext.OverAllCommentss.Where(x => x.ColorCommentId == colorId && x.IsDelete == false).ToList();

        }
        //public OverAllComments GetByColorId(int colorId)
        //{
        //    return _databaseContext.OverAllCommentss.SingleOrDefault(x => x.ColorCommentId == colorId && x.IsDelete == false);

        //}

        public void Insert(OverAllComments model)
        {
            _databaseContext.OverAllCommentss.Add(model);
        }

        public bool IsExist(int id, string overAllComment)
        {
            var results = _databaseContext.OverAllCommentss.Where(x => x.OverAllComment == overAllComment && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(string overAllComment)
        {
            var results = _databaseContext.OverAllCommentss.Where(x => x.OverAllComment == overAllComment && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(OverAllComments model)
        {
            _databaseContext.OverAllCommentss.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
