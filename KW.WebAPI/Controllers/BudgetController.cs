using KW.Application;
using KW.Application.DTO;
using KW.Application.Params;
using KW.Common;
using KW.Domain;
using KW.Presentation.WebAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace KW.Presentation.WebAPI.Controllers
{
    public class BudgetController : BaseAPIController
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        //GET api/budget
        [HttpGet]
        public IHttpActionResult Get([FromUri] BudgetListParameter param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IList<Budget> budgets = _budgetService.GetAll().ToList();
                    if (param.IsPagination())
                    {
                        string keyword = string.Empty;
                        string field = string.Empty;

                        param.Validate();
                        keyword = param.Search;
                        field = param.SearchBy;

                        int skip = 0;
                        if (param.PageNo > 0)
                        {
                            skip = (param.PageNo - 1) * param.PageSize;
                        }

                        int totalRows = _budgetService.GetAll().Count();
                        var totalPage = totalRows / param.PageSize;
                        var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
                        if (totalPages < 0)
                            totalPages = 0;

                        var result = budgets.Skip(skip).Take(param.PageSize).ToList();

                        IList<BudgetDTO> colls = BudgetDTO.From(result);

                        PaginationDTO page = new PaginationDTO();
                        page.PageCount = totalPages;
                        page.PageNo = param.PageNo;
                        page.PageSize = param.PageSize;
                        page.results = colls;

                        return Ok(page);
                    }
                    else
                    {
                        IList<BudgetDTO> dto = BudgetDTO.From(budgets);
                        return Ok(dto);
                    }

                }
                else
                {
                    string errorResult = string.Join(" ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return Content(HttpStatusCode.BadRequest, errorResult);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //GET api/sektor/1
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _budgetService.Get(id);
                BudgetDTO budgetDTO = BudgetDTO.From(result);
                return Ok(budgetDTO);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //POST api/budget
        [HttpPost]
        public IHttpActionResult Add(BudgetParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreatedDate = getDate;
                    param.CreatedBy = userId;

                    int id = _budgetService.Add(param);
                    return Ok(id);
                }
                else
                {
                    string errorResult = string.Join(" ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return Content(HttpStatusCode.BadRequest, errorResult);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //PUT api/budget/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]BudgetParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.UpdatedDate = getDate;
                    param.UpdatedBy = userId;

                    int result = _budgetService.Update(id, param);
                    return Ok(result);
                }
                else
                {
                    string errorResult = string.Join(" ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return Content(HttpStatusCode.BadRequest, errorResult);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //DELETE api/budget/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();
                    int result = _budgetService.Delete(id, userId, getDate);
                    return Ok(result);
                }
                else
                {
                    string errorResult = string.Join(" ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return Content(HttpStatusCode.BadRequest, errorResult);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }
    }
}
