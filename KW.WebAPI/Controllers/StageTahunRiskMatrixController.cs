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
    public class StageTahunRiskMatrixController : BaseAPIController
    {
        private readonly IStageTahunRiskMatrixService _stageTahunRiskMatrixService;
        private readonly IRiskRegistrasiService _riskRegistrasiService;

        public StageTahunRiskMatrixController(IStageTahunRiskMatrixService stageTahunRiskMatrixService, IRiskRegistrasiService riskRegistrasiService)
        {
            _stageTahunRiskMatrixService = stageTahunRiskMatrixService;
            _riskRegistrasiService = riskRegistrasiService;
        }

        //GET api/stageTahunRiskMatrix
        [HttpGet]
        public IHttpActionResult Get([FromUri] StageTahunRiskMatrixListParameter param)
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

                    int totalRows = _stageTahunRiskMatrixService.GetAll().Count();
                    var totalPage = totalRows / param.PageSize;
                    var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
                    if (totalPages < 0)
                        totalPages = 0;

                    var result = _stageTahunRiskMatrixService.GetAll()
                        .Skip(skip)
                        .Take(param.PageSize)
                        .ToList();

                    var riskRegistrasi = _riskRegistrasiService.GetAll().ToList();
                    IList<StageTahunRiskMatrixDTO> colls = StageTahunRiskMatrixDTO.From(result, riskRegistrasi);

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

        //GET api/stageTahunRiskMatrix/1
        //[HttpGet]
        //public IHttpActionResult Get(int id)
        //{
        //    try
        //    {
        //        var result = _stageTahunRiskMatrixService.Get(id);
        //        var riskRegistrasi = _riskRegistrasiService.GetAll().ToList();

        //        StageTahunRiskMatrixDTO stageTahunRiskMatrixDTO = StageTahunRiskMatrixDTO.From(result, riskRegistrasi);
        //        return Ok(stageTahunRiskMatrixDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException == null)
        //            return Content(HttpStatusCode.InternalServerError, ex.Message);
        //        else
        //            return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
        //    }
        //}

        //GET api/stageTahunRiskMatrix/1
        [HttpGet]
        public IHttpActionResult GetRiskMatrixProjectId(int id)
        {
            try
            {
                //var result = _stageTahunRiskMatrixService.GetByRiskMatrixProjectId(id).ToList();
                //var riskRegistrasi = _riskRegistrasiService.GetAll().ToList();
                //IList<StageTahunRiskMatrixDTO> stageTahunRiskMatrixDTO = StageTahunRiskMatrixDTO.From(result, riskRegistrasi);
                //return Ok(stageTahunRiskMatrixDTO);

                var result = _stageTahunRiskMatrixService.GetByRiskMatrixProjectId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //POST api/stageTahunRiskMatrix
        [HttpPost]
        public IHttpActionResult Add(StageTahunRiskMatrixParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _stageTahunRiskMatrixService.Add(param);
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

        //PUT api/stageTahunRiskMatrix/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]StageTahunRiskMatrixParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.UpdateDate = getDate;
                    param.UpdateBy = userId;

                    int result = _stageTahunRiskMatrixService.Update(id, param);
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

        //DELETE api/stageTahunRiskMatrix/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();
                    int result = _stageTahunRiskMatrixService.Delete(id, userId, getDate);
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
