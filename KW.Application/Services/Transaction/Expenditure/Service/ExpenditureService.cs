using KW.Common.Validation;
using System;
using System.Collections.Generic;
using KW.Application.Params;
using KW.Core;
using KW.Domain;

namespace KW.Application
{
    public class ExpenditureService : IExpenditureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpenditureRepository _expenditureRepository;
        private readonly IExpenditureDetailRepository _expenditureDetailRepository;
        private readonly IBudgetRepository _budgetRepository;

        public ExpenditureService(IUnitOfWork uow, IExpenditureRepository expenditureRepository, IExpenditureDetailRepository expenditureDetailRepository, IBudgetRepository budgetRepository)
        {
            _unitOfWork = uow;
            _expenditureRepository = expenditureRepository;
            _expenditureDetailRepository = expenditureDetailRepository;
            _budgetRepository = budgetRepository;
        }

        public IEnumerable<Expenditure> GetAll()
        {
            return _expenditureRepository.GetAll();
        }

        public Expenditure Get(int id)
        {
            return _expenditureRepository.Get(id);
        }

        public int Add(ExpenditureParam param)
        {
            int id = 0;
            Validate.NotNull(param.ExpenditureName, "Expenditure name is required.");

            var budget = _budgetRepository.Get(param.BudgetId);
            Validate.NotNull(budget, "Budget name is not found.");

            isExist(param.ExpenditureDate);
            using (_unitOfWork)
            {
                Expenditure model = new Expenditure(param.ExpenditureDate, param.CreatedBy, param.CreatedDate);
                _expenditureRepository.Insert(model);

                //insert detail

                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Update(int id, ExpenditureParam param)
        {
            var model = _expenditureRepository.Get(id);
            Validate.NotNull(model, "Expenditure name is not found.");

            var budget = _budgetRepository.Get(param.BudgetId);
            Validate.NotNull(budget, "Budget name is not found.");

            isExist(id, param.ExpenditureDate);
            using (_unitOfWork)
            {
                model.Update(param.ExpenditureDate, param.UpdatedBy, param.UpdatedDate);
                _expenditureRepository.Update(model);

                _unitOfWork.Commit();
            }
            return model.Id;
        }

        public void IsDetailExist(int expenditureId)
        {
            //if (_expenditureDetailRepository.IsExist())
        }

        public void IsDetailExist(int expenditureId, string expenditureName)
        {

        }

        public void isExist(DateTime expenditureDate)
        {
            if (_expenditureRepository.IsExist(expenditureDate))
            {
                throw new ApplicationException(string.Format("Expenditure date for {0} already exist.", expenditureDate.Date));
            }
        }

        public void isExist( int id, DateTime expenditureDate)
        {
            if (_expenditureRepository.IsExist(id, expenditureDate))
            {
                throw new ApplicationException(string.Format("Expenditure date for {0} already exist.", expenditureDate.Date));
            }
        }

        public int Delete(int id, int updatedBy, DateTime updatedDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Expenditure name does is not found.");

            using (_unitOfWork)
            {
                model.Delete(updatedBy, updatedDate);
                _expenditureRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
    }
}
