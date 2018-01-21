using KW.Common.Validation;
using System;
using System.Collections.Generic;
using KW.Application.Params;
using KW.Core;
using KW.Domain;
using System.Linq;

namespace KW.Application
{
    public class ExpenditureDetailService : IExpenditureDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpenditureRepository _expenditureRepository;
        private readonly IExpenditureDetailRepository _expenditureDetailRepository;
        private readonly IBudgetRepository _budgetRepository;

        public ExpenditureDetailService(IUnitOfWork uow, IExpenditureRepository expenditureRepository, IExpenditureDetailRepository expenditureDetailRepository, IBudgetRepository budgetRepository)
        {
            _unitOfWork = uow;
            _expenditureRepository = expenditureRepository;
            _expenditureDetailRepository = expenditureDetailRepository;
            _budgetRepository = budgetRepository;
        }

        public IEnumerable<ExpenditureDetail> GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExpenditureDetail> GetByExpenditureId(int expenditureId)
        {
            return _expenditureDetailRepository.GetByExpenditureId(expenditureId);
        }

        public ExpenditureDetail Get(int id)
        {
            return _expenditureDetailRepository.Get(id);
        }
    }
}
