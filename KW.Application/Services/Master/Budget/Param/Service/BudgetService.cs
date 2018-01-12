using KW.Common.Validation;
using System;
using System.Collections.Generic;
using KW.Application.Params;
using KW.Core;
using KW.Domain;

namespace KW.Application
{
    public class BudgetService : IBudgetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IUnitOfWork uow, IBudgetRepository budgetRepository)
        {
            _unitOfWork = uow;
            _budgetRepository = budgetRepository;
        }

        public IEnumerable<Budget> GetAll()
        {
            return _budgetRepository.GetAll();
        }

        public Budget Get(int id)
        {
            return _budgetRepository.Get(id);
        }

        public int Add(BudgetParam param)
        {
            int id = 0;
            Validate.NotNull(param.BudgetName, "Budget name is required.");

            isExisit(param.BudgetName);
            using (_unitOfWork)
            {
                Budget model = new Budget(param.BudgetName, param.Definition, param.CreatedBy, param.CreatedDate);
                _budgetRepository.Insert(model);

                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Update(int id, BudgetParam param)
        {
            var model = _budgetRepository.Get(id);
            Validate.NotNull(model, "Budget name is not found.");

            isExisit(id, param.BudgetName);
            using (_unitOfWork)
            {
                model.Update(param.BudgetName, param.Definition, param.UpdatedBy, param.UpdatedDate);
                _budgetRepository.Update(model);

                _unitOfWork.Commit();
            }
            return model.Id;
        }

        public void isExisit(string budgetName)
        {
            if (_budgetRepository.IsExist(budgetName))
            {
                throw new ApplicationException(string.Format("Budget name {0} already exist.", budgetName));
            }
        }

        public void isExisit( int id, string budgetName)
        {
            if (_budgetRepository.IsExist(id, budgetName))
            {
                throw new ApplicationException(string.Format("Budget name {0} already exist.", budgetName));
            }
        }
    }
}
