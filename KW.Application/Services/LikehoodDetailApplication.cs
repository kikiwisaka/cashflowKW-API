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
    public class LikehoodDetailService : ILikehoodDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILikehoodRepository _likehoodRepository;
        private readonly ILikehoodDetailRepository _likehoodDetailRepository;

        public LikehoodDetailService(IUnitOfWork uow, ILikehoodRepository likehoodRepository, ILikehoodDetailRepository likehoodDetailRepository)
        {
            _unitOfWork = uow;
            _likehoodRepository = likehoodRepository;
            _likehoodDetailRepository = likehoodDetailRepository;
        }

        #region Query
        public IEnumerable<LikehoodDetail> GetAll()
        {
            return _likehoodDetailRepository.GetAll();
        }

        public IEnumerable<LikehoodDetail> GetByLikehoodId(int likehoodId)
        {
            return _likehoodDetailRepository.GetByLikehoodId(likehoodId);
        }

        public LikehoodDetail Get(int id)
        {
            return _likehoodDetailRepository.Get(id);
        }

        public void IsExistOnEditing(int id, string definisiLikehood)
        {
            if (_likehoodDetailRepository.IsExist(id, definisiLikehood))
            {
                throw new ApplicationException(string.Format("Definisi Likehood {0} sudah ada.", definisiLikehood));
            }
        }

        public void isExistOnAdding(string definisiLikehood)
        {
            if (_likehoodDetailRepository.IsExist(definisiLikehood))
            {
                throw new ApplicationException(string.Format("Definisi Likehood {0} sudah ada.", definisiLikehood));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(LikehoodDetailParam param)
        {
            int id;
            Validate.NotNull(param.DefinisiLikehood, "Definisi Likehood wajib diisi.");

            isExistOnAdding(param.DefinisiLikehood);
            using (_unitOfWork)
            {
                LikehoodDetail model = new LikehoodDetail(param.DefinisiLikehood, param.Lower, param.Upper, param.Average, param.LikehoodId, param.CreateBy, param.CreateDate);
                _likehoodDetailRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, LikehoodDetailParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Definisi Likehood tidak ditemukan.");

            //IsExistOnEditing(id, param.DefinisiLikehood);
            using (_unitOfWork)
            {
                model.Update(param.DefinisiLikehood, param.Lower, param.Upper, param.Average, model.LikehoodId, param.UpdateBy, param.UpdateDate);
                _likehoodDetailRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        #endregion Manipulation
    }
}
