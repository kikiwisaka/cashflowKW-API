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
    public class ColorCommentService : IColorCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IColorCommentRepository _colorCommentRepository;

        public ColorCommentService(IUnitOfWork uow, IColorCommentRepository colorCommentRepository)
        {
            _unitOfWork = uow;
            _colorCommentRepository = colorCommentRepository;
        }

        #region Query
        public IEnumerable<ColorComment> GetAll()
        {
            return _colorCommentRepository.GetAll();
        }

        public ColorComment Get(int id)
        {
            return _colorCommentRepository.Get(id);
        }

        public void IsExistOnEditing(int id, string warna)
        {
            if (_colorCommentRepository.IsExist(id, warna))
            {
                throw new ApplicationException(string.Format("Warna {0} sudah ada.", warna));
            }
        }

        public void isExistOnAdding(string warna)
        {
            if (_colorCommentRepository.IsExist(warna))
            {
                throw new ApplicationException(string.Format("Warna {0} sudah ada.", warna));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(ColorCommentParam param)
        {
            int id;
            Validate.NotNull(param.Warna, "Warna wajib diisi.");

            //count average here 
            isExistOnAdding(param.Warna);
            using (_unitOfWork)
            {
                ColorComment model = new ColorComment(param.Warna, param.CreateBy, param.CreateDate);
                _colorCommentRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, ColorCommentParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Warna tidak ditemukan.");

            IsExistOnEditing(id, param.Warna);
            using (_unitOfWork)
            {
                model.Update(param.Warna, param.UpdateBy, param.UpdateDate);
                _colorCommentRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Warna tidak ditemukan.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _colorCommentRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
