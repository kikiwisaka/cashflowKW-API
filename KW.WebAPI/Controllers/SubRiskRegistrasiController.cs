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
    public class SubRiskRegistrasiController : BaseAPIController
    {
        private readonly ISubRiskRegistrasiService _subRiskRegistrasiService;

        public SubRiskRegistrasiController(ISubRiskRegistrasiService subRiskRegistrasiService)
        {
            _subRiskRegistrasiService = subRiskRegistrasiService;
        }

        //GET api/subRiskRegistrasi
        [HttpGet]
        public IHttpActionResult Get([FromUri] SubRiskRegistrasiListParameter param)
        {
            try
            {
                if (ModelState.IsValid)
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

                    int totalRows = _subRiskRegistrasiService.GetByRiskId(param.ParentId).Count();
                    var totalPage = totalRows / param.PageSize;
                    var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
                    if (totalPages < 0)
                        totalPages = 0;

                    var result = _subRiskRegistrasiService.GetByRiskId(param.ParentId)
                        .Skip(skip)
                        .Take(param.PageSize)
                        .ToList();

                    IList<SubRiskRegistrasiDTO> colls = SubRiskRegistrasiDTO.From(result);

                    PaginationDTO page = new PaginationDTO();
                    page.PageCount = totalPages;
                    page.PageNo = param.PageNo;
                    page.PageSize = param.PageSize;
                    page.results = colls;

                    return Ok(page);
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

        //GET api/subRiskRegistrasi/1
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _subRiskRegistrasiService.Get(id);
                SubRiskRegistrasiDTO subRiskRegistrasiDTO = SubRiskRegistrasiDTO.From(result);
                return Ok(subRiskRegistrasiDTO);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //GET api/subRiskRegistrasi?riskId={id}
        [HttpGet]
        public IHttpActionResult GetByRiskId(int riskId, int PageNo, int PageSize)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    int skip = 0;
                    if (PageNo > 1)
                    {
                        skip = PageNo * PageSize;
                    }

                    var result = _subRiskRegistrasiService.GetByRiskId(riskId).ToList();

                    int totalRows = result.Count();
                    var totalPage = totalRows / PageSize;
                    var totalPages = (int)Math.Ceiling((double)totalRows / PageSize) - 1;
                    if (totalPages < 0)
                        totalPages = 0;

                    IList<SubRiskRegistrasiDTO> subRiskDTO = SubRiskRegistrasiDTO.From(result);

                    PaginationDTO page = new PaginationDTO();
                    page.PageCount = totalPages;
                    page.PageNo = PageNo;
                    page.PageSize = PageSize;
                    page.results = subRiskDTO;

                    return Ok(page);
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

        //POST api/subRiskRegistrasi
        [HttpPost]
        public IHttpActionResult Add(SubRiskRegistrasiParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _subRiskRegistrasiService.Add(param);
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

        //PUT api/subRiskRegistrasi/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]SubRiskRegistrasiParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.UpdateDate = getDate;
                    param.UpdateBy = userId;

                    int result = _subRiskRegistrasiService.Update(id, param);
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

        //DELETE api/subRiskRegistrasi/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();
                    int result = _subRiskRegistrasiService.Delete(id, userId, getDate);
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
