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
    public class OverAllCommentsService : IOverAllCommentsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOverAllCommentsRepository _overAllCommentsRepository;
        private readonly IColorCommentRepository _colorCommentRepository;
        private readonly IMatrixRepository _matrixRepository;

        public OverAllCommentsService(IUnitOfWork uow, IOverAllCommentsRepository overAllCommentsRepository, IColorCommentRepository colorCommentRepository)
        {
            _unitOfWork = uow;
            _overAllCommentsRepository = overAllCommentsRepository;
            _colorCommentRepository = colorCommentRepository;
        }

        #region Query
        public IEnumerable<OverAllComments> GetAll()
        {
            return _overAllCommentsRepository.GetAll();
        }
        public IEnumerable<OverAllComments> GetByColorId(int colorId)
        {
            return _overAllCommentsRepository.GetByColorId(colorId);
        }
        public OverAllComments Get(int id)
        {
            return _overAllCommentsRepository.Get(id);
        }

        public void IsExistOnEditing(int id, string overAllComment)
        {
            if (_overAllCommentsRepository.IsExist(id, overAllComment))
            {
                throw new ApplicationException(string.Format("OverAllComment {0} sudah ada.", overAllComment));
            }
        }

        public void isExistOnAdding(string overAllComment)
        {
            if (_overAllCommentsRepository.IsExist(overAllComment))
            {
                throw new ApplicationException(string.Format("OverAllComment {0} sudah ada.", overAllComment));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(OverAllCommentsParam param)
        {
            int id;
            Validate.NotNull(param.OverAllComment, "OverAllComment wajib diisi.");

            var colorComment = _colorCommentRepository.Get(param.ColorCommentId);

            //count average here 
            isExistOnAdding(param.OverAllComment);
            using (_unitOfWork)
            {
                OverAllComments model = new OverAllComments(colorComment, param.OverAllComment, param.CreateBy, param.CreateDate);
                _overAllCommentsRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, OverAllCommentsParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "OverAllComment tidak ditemukan.");

            var colorComment = _colorCommentRepository.Get(param.ColorCommentId);

            IsExistOnEditing(id, param.OverAllComment);
            using (_unitOfWork)
            {
                model.Update(colorComment, param.OverAllComment, param.UpdateBy, param.UpdateDate);
                _overAllCommentsRepository.Update(model);
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
                _overAllCommentsRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
