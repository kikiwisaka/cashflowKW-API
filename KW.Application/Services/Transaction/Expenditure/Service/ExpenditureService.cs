using KW.Common.Validation;
using System;
using System.Collections.Generic;
using KW.Application.Params;
using KW.Core;
using KW.Domain;
using System.Linq;

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

            Expenditure modelHeader = new Expenditure();

            using (_unitOfWork)
            {
                var currentData = _expenditureRepository.GetByExpenditureDate(param.ExpenditureDate);
                if (currentData == null)
                {
                    Expenditure model = new Expenditure(param.ExpenditureDate, param.Price, param.CreatedBy, param.CreatedDate);
                    _expenditureRepository.Insert(model);
                    id = model.Id;
                    modelHeader = model;

                    //insert detail
                    ExpenditureDetail modelDetail = new ExpenditureDetail(param.ExpenditureName, param.ExpenditureDefinition, param.Price, model, budget, param.CreatedBy, param.CreatedDate);
                    _expenditureDetailRepository.Insert(modelDetail);
                }
                else
                {
                    //insert detail
                    ExpenditureDetail modelDetail = new ExpenditureDetail(param.ExpenditureName, param.ExpenditureDefinition, param.Price, modelHeader, budget, param.CreatedBy, param.CreatedDate);
                    _expenditureDetailRepository.Insert(modelDetail);

                    //add total
                    var dataHeader = _expenditureRepository.Get(param.ExpenditureId);
                    var totalPerDay = CalculateTotalPerDay(param.ExpenditureId);

                    var grandTotal = dataHeader.Total + totalPerDay;
                    dataHeader.AddTotal(grandTotal);
                }

                _unitOfWork.Commit();
            }
            return id;
        }

        public int Update(int id, ExpenditureParam param)
        {
            var model = _expenditureRepository.Get(id);
            Validate.NotNull(model, "Expenditure name is not found.");

            var budget = _budgetRepository.Get(param.BudgetId);
            Validate.NotNull(budget, "Budget name is not found.");

            var modelDetail = _expenditureDetailRepository.GetByExpenditureIdExpenditureDetailId(id, param.ExpenditureDetailId);

            //isExist(id, param.ExpenditureDate);
            using (_unitOfWork)
            {
                var currentDate = _expenditureRepository.GetByExpenditureDate(param.ExpenditureDate);
                if (currentDate == null)
                {
                    Expenditure modelNew = new Expenditure(param.ExpenditureDate, param.Price, param.CreatedBy, param.CreatedDate);
                    _expenditureRepository.Insert(modelNew);

                    model = modelNew;
                }
                if (modelDetail != null)
                {
                    modelDetail.Update(param.ExpenditureName, param.ExpenditureDefinition, param.Price, model, budget, param.UpdatedBy, param.UpdatedDate);
                    _expenditureDetailRepository.Update(modelDetail);

                    //change total
                    var dataHeader = _expenditureRepository.Get(modelDetail.ExpenditureId);
                    var totalPerDay = CalculateTotalPerDay(modelDetail.ExpenditureId);
                    var grandTotal = dataHeader.Total + totalPerDay;

                    dataHeader.AddTotal(grandTotal);
                }
                else
                {
                    Validate.NotNull(modelDetail, "Expenditure Detail's id {1} with expenditure's id {0} is not found. ", model.Id, modelDetail.Id);
                }

                _unitOfWork.Commit();
            }
            return model.Id;
        }

        public int Delete(int expenditureDetailId, int updatedBy, DateTime updatedDate)
        {
            var modelDetail = _expenditureDetailRepository.Get(expenditureDetailId);
            Validate.NotNull(modelDetail, "Expenditure name does not found.");

            var model = this.Get(modelDetail.ExpenditureId);

            var data = _expenditureDetailRepository.GetByExpenditureId(expenditureDetailId).ToList();
           
            using (_unitOfWork)
            {
                modelDetail.Delete(updatedBy, updatedDate);
                _expenditureDetailRepository.Update(modelDetail);

                if (data.Count == 1)
                    model.Delete(updatedBy, updatedDate);

                _unitOfWork.Commit();
                expenditureDetailId = modelDetail.Id;
            }
            return expenditureDetailId;
        }

        public double CalculateTotalPerDay(int expenditureId)
        {
            double total = 0;
            var dataDetail = _expenditureDetailRepository.GetByExpenditureId(expenditureId).ToList();
            total = dataDetail.Sum(x => x.Price);

            return total;
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

        
    }
}
