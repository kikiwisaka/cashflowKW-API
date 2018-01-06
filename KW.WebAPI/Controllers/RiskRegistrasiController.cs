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
    public class RiskRegistrasiController : BaseAPIController
    {
        private readonly IRiskRegistrasiService _riskRegistrasiService;

        public RiskRegistrasiController(IRiskRegistrasiService riskRegistrasiService)
        {
            _riskRegistrasiService = riskRegistrasiService;
        }

        //GET api/riskRegistrasi
        [HttpGet]
        public IHttpActionResult Get([FromUri] RiskRegistrasiListParameter param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IList<RiskRegistrasi> risks = _riskRegistrasiService.GetAll().ToList();
                    if(param.IsPagination())
                    {
                        string keyword = string.Empty;
                        string field = string.Empty;

                        param.Validate();
                        keyword = param.Search;
                        field = param.SearchBy;

                        int skip = 0;
                        if (param.PageNo > 0)
                            skip = (param.PageNo - 1) * param.PageSize;

                        int totalRows = _riskRegistrasiService.GetAll().Count();
                        var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
                        if (totalPages < 0)
                            totalPages = 0;

                        var result = risks.Skip(skip).Take(param.PageSize).ToList();

                        IList<RiskRegistrasiDTO> colls = RiskRegistrasiDTO.From(result);

                        PaginationDTO page = new PaginationDTO();
                        page.PageCount = totalPages;
                        page.PageNo = param.PageNo;
                        page.PageSize = param.PageSize;
                        page.results = colls;

                        return Ok(page);
                    }
                    else
                    {
                        IList<RiskRegistrasiDTO> dto = RiskRegistrasiDTO.From(risks);
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

        //GET api/riskRegistrasi/1
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _riskRegistrasiService.Get(id);
                RiskRegistrasiDTO riskRegistrasiDTO = RiskRegistrasiDTO.From(result);
                return Ok(riskRegistrasiDTO);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //POST api/riskRegistrasi
        [HttpPost]
        public IHttpActionResult Add(RiskRegistrasiParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _riskRegistrasiService.Add(param);
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

        //PUT api/riskRegistrasi/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]RiskRegistrasiParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.UpdateDate = getDate;
                    param.UpdateBy = userId;

                    int result = _riskRegistrasiService.Update(id, param);
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

        //DELETE api/riskRegistrasi/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();
                    int result = _riskRegistrasiService.Delete(id, userId, getDate);
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
