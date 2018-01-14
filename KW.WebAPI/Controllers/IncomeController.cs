using KW.Application;
using KW.Application.DTO;
using KW.Application.Params;
using KW.Common;
using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace KW.Presentation.WebAPI.Controllers
{
    public class IncomeController : BaseAPIController
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        //GET api/income
        [HttpGet]
        public IHttpActionResult Get([FromUri] IncomeListParameter param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IList<Income> incomes = _incomeService.GetAll().ToList();
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

                        int totalRows = _incomeService.GetAll().Count();
                        var totalPage = totalRows / param.PageSize;
                        var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
                        if (totalPages < 0)
                            totalPages = 0;

                        var result = incomes.Skip(skip).Take(param.PageSize).ToList();

                        IList<IncomeDTO> colls = IncomeDTO.From(result);

                        PaginationDTO page = new PaginationDTO();
                        page.PageCount = totalPages;
                        page.PageNo = param.PageNo;
                        page.PageSize = param.PageSize;
                        page.results = colls;

                        return Ok(page);
                    }
                    else
                    {
                        IList<IncomeDTO> dto = IncomeDTO.From(incomes);
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
                var result = _incomeService.Get(id);
                IncomeDTO incomeDTO = IncomeDTO.From(result);
                return Ok(incomeDTO);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //POST api/income
        [HttpPost]
        public IHttpActionResult Add(IncomeParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreatedDate = getDate;
                    param.CreatedBy = userId;

                    int id = _incomeService.Add(param);
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

        //PUT api/income/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]IncomeParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.UpdatedDate = getDate;
                    param.UpdatedBy = userId;

                    int result = _incomeService.Update(id, param);
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

        //DELETE api/income/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();
                    int result = _incomeService.Delete(id, userId, getDate);
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
