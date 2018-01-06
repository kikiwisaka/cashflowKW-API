using KW.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KW.Application.Params;
using KW.Core;
using KW.Domain;

namespace KW.Application
{
    public class CommentsService : ICommentsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IColorCommentRepository _colorCommentRepository;
        private readonly IMatrixRepository _matrixRepository;

        public CommentsService(IUnitOfWork uow, ICommentsRepository commentsRepository, IColorCommentRepository colorCommentRepository, IMatrixRepository matrixRepository)
        {
            _unitOfWork = uow;
            _commentsRepository = commentsRepository;
            _colorCommentRepository = colorCommentRepository;
            _matrixRepository = matrixRepository;
        }

        #region Query
        public IEnumerable<Comments> GetAll()
        {
            return _commentsRepository.GetAll();
        }
        public IEnumerable<Comments> GetByColorId(int colorId)
        {
            return _commentsRepository.GetByColorId(colorId);
        }
        public Comments Get(int id)
        {
            return _commentsRepository.Get(id);
        }

        public void IsExistOnEditing(int id, string comment)
        {
            if (_commentsRepository.IsExist(id, comment))
            {
                throw new ApplicationException(string.Format("Comment {0} sudah ada.", comment));
            }
        }

        public void isExistOnAdding(string comment)
        {
            if (_commentsRepository.IsExist(comment))
            {
                throw new ApplicationException(string.Format("Comment {0} sudah ada.", comment));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(CommentsParam param)
        {
            int id;
            Validate.NotNull(param.Comment, "Comment wajib diisi.");

            var colorComment = _colorCommentRepository.Get(param.ColorCommentId);
            var matrix = _matrixRepository.Get(param.MatrixId);

            //count average here 
            isExistOnAdding(param.Comment);
            using (_unitOfWork)
            {
                Comments model = new Comments(colorComment, matrix, param.Comment, param.ActionPoint, param.CreateBy, param.CreateDate);
                _commentsRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, CommentsParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Comment tidak ditemukan.");

            var colorComment = _colorCommentRepository.Get(param.ColorCommentId);
            var matrix = _matrixRepository.Get(param.MatrixId);

            IsExistOnEditing(id, param.Comment);
            using (_unitOfWork)
            {
                model.Update(colorComment, matrix, param.Comment, param.ActionPoint, param.UpdateBy, param.UpdateDate);
                _commentsRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Comment tidak ditemukan.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _commentsRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
