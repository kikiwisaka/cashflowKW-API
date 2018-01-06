using KW.Common.Validation;
using System;
using System.Collections.Generic;
using KW.Application.Params;
using KW.Core;
using KW.Domain;

namespace KW.Application
{
    public class LikehoodService : ILikehoodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILikehoodRepository _likehoodRepository;
        private readonly ILikehoodDetailRepository _likehoodDetailRepository;

        public LikehoodService(IUnitOfWork uow, ILikehoodRepository likehoodRepository, ILikehoodDetailRepository likehoodDetailRepository)
        {
            _unitOfWork = uow;
            _likehoodRepository = likehoodRepository;
            _likehoodDetailRepository = likehoodDetailRepository;
        }

        #region Query
        public IEnumerable<Likehood> GetAll()
        {
            return _likehoodRepository.GetAll();
        }

        public Likehood Get(int id)
        {
            return _likehoodRepository.Get(id);
        }

        public Likehood GetDefault()
        {
            return _likehoodRepository.GetDefault();
        }

        public void IsExistOnEditing(int id, string namaLikehood)
        {
            if (_likehoodRepository.IsExist(id, namaLikehood))
            {
                throw new ApplicationException(string.Format("Nama Likehood {0} sudah ada.", namaLikehood));
            }
        }

        public void isExistOnAdding(string namaLikehood)
        {
            if (_likehoodRepository.IsExist(namaLikehood))
            {
                throw new ApplicationException(string.Format("Nama Likehood {0} sudah ada.", namaLikehood));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(LikehoodParam param)
        {
            int id;
            Validate.NotNull(param.NamaLikehood, "Nama Likehood wajib diisi.");

            //count average here 
            isExistOnAdding(param.NamaLikehood);

            //Definition Likelihoods
            string[] definition = { "VL", "L", "M", "H", "VH" };
            IList<LikehoodDetail> likehoodDetails = new List<LikehoodDetail>();

            using (_unitOfWork)
            {
                Likehood model = new Likehood(param.NamaLikehood, param.CreateBy, param.CreateDate);
                _likehoodRepository.Insert(model);

                foreach (var item in definition)
                {
                    LikehoodDetail modelDetail = new LikehoodDetail(item, 0, 0, 0, model.Id, param.CreateBy, param.CreateDate);
                    likehoodDetails.Add(modelDetail);
                    _likehoodDetailRepository.Insert(modelDetail);
                }

                model.AddLikehoodDetail(likehoodDetails);

                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, LikehoodParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Likehood tidak ditemukan.");

            IsExistOnEditing(id, param.NamaLikehood);
            using (_unitOfWork)
            {
                model.Update(param.NamaLikehood, param.UpdateBy, param.UpdateDate);
                _likehoodRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Likehood tidak ditemukan.");

            var modelDetail = _likehoodDetailRepository.GetByLikehoodId(id);
            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _likehoodRepository.Update(model);

                foreach (var item in modelDetail)
                {
                    item.Delete(model.Id, deleteBy, deleteDate);
                }

                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Default(int id, LikehoodParam param)
        {
            int modelId;
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Likehood is not found.");

            var modelDefault = GetDefault();

            using (_unitOfWork)
            {
                if (modelDefault != null)
                {
                    //Remove current default
                    modelDefault.RemoveDefault(param.UpdateBy, param.UpdateDate);
                    _likehoodRepository.Update(modelDefault);
                }
                //Set new default
                model.SetDefault(param.UpdateBy, param.UpdateDate);
                _likehoodRepository.Update(model);

                _unitOfWork.Commit();
            }
            
            modelId = model.Id;

            return modelId;
        }
        #endregion Manipulation
    }
}
