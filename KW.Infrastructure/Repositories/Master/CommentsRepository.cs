using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public CommentsRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Comments Get(int id)
        {
            return _databaseContext.Commentss.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Comments> GetAll()
        {
            //return _databaseContext.Commentss.AsQueryable();
            return _databaseContext.Commentss.Where(x => x.IsDelete == false).ToList();

        }
        public IEnumerable<Comments> GetByColorId(int colorId)
        {
            return _databaseContext.Commentss.Where(x => x.ColorCommentId == colorId && x.IsDelete == false).ToList();

        }

        public void Insert(Comments model)
        {
            _databaseContext.Commentss.Add(model);
        }

        public bool IsExist(int id, string comment)
        {
            var results = _databaseContext.Commentss.Where(x => x.Comment.ToLower() == comment.ToLower() && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(string comment)
        {
            var results = _databaseContext.Commentss.Where(x => x.Comment == comment && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(Comments model)
        {
            _databaseContext.Commentss.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
