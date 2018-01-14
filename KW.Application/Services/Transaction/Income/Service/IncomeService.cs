using KW.Common.Validation;
using System;
using System.Collections.Generic;
using KW.Application.Params;
using KW.Core;
using KW.Domain;

namespace KW.Application
{
    public class IncomeService : IIncomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIncomeRepository _incomeRepository;
        private readonly IBudgetRepository _budgetRepository;

        public IncomeService(IUnitOfWork uow, IIncomeRepository incomeRepository, IBudgetRepository budgetRepository)
        {
            _unitOfWork = uow;
            _incomeRepository = incomeRepository;
            _budgetRepository = budgetRepository;
        }

        public IEnumerable<Income> GetAll()
        {
            return _incomeRepository.GetAll();
        }

        public Income Get(int id)
        {
            return _incomeRepository.Get(id);
        }

        public int Add(IncomeParam param)
        {
            int id = 0;
            Validate.NotNull(param.IncomeName, "Income name is required.");

            var budget = _budgetRepository.Get(param.BudgetId);
            Validate.NotNull(budget, "Budget name is not found.");

            isExist(param.IncomeName, param.IncomeDate);
            using (_unitOfWork)
            {
                Income model = new Income(param.IncomeName, param.Definition, param.IncomeDate, budget, param.CreatedBy, param.CreatedDate);
                _incomeRepository.Insert(model);

                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Update(int id, IncomeParam param)
        {
            var model = _incomeRepository.Get(id);
            Validate.NotNull(model, "Income name is not found.");

            var budget = _budgetRepository.Get(param.BudgetId);
            Validate.NotNull(budget, "Budget name is not found.");

            isExist(id, param.IncomeName, param.IncomeDate);
            using (_unitOfWork)
            {
                model.Update(param.IncomeName, param.Definition, param.IncomeDate, budget,  param.UpdatedBy, param.UpdatedDate);
                _incomeRepository.Update(model);

                _unitOfWork.Commit();
            }
            return model.Id;
        }

        public void isExist(string incomeName, DateTime date)
        {
            if (_incomeRepository.IsExist(incomeName, date))
            {
                throw new ApplicationException(string.Format("Income name {0} for month {1} already exist.", incomeName, date.Month));
            }
        }

        public void isExist( int id, string incomeName, DateTime date)
        {
            if (_incomeRepository.IsExist(id, incomeName, date))
            {
                throw new ApplicationException(string.Format("Income name {0} for month {1} already exist.", incomeName, date.Month));
            }
        }

        public int Delete(int id, int updatedBy, DateTime updatedDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Income name does is not found.");

            using (_unitOfWork)
            {
                model.Delete(updatedBy, updatedDate);
                _incomeRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
    }
}
