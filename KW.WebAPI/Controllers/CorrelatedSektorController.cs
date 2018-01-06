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
    public class CorrelatedSektorController : BaseAPIController
    {
        private readonly ICorrelatedSektorService _correlatedSektorService;
        private readonly ICorrelationMatrixService _correlationMatrixService;
        private readonly IRiskRegistrasiService _riskRegistrasiService;
        private readonly IScenarioService _scenarioService;

        public CorrelatedSektorController(ICorrelatedSektorService correlatedSektorService, ICorrelationMatrixService correlationMatrixService, IRiskRegistrasiService riskRegistrasiService, IScenarioService scenarioService)
        {
            _correlatedSektorService = correlatedSektorService;
            _correlationMatrixService = correlationMatrixService;
            _riskRegistrasiService = riskRegistrasiService;
            _scenarioService = scenarioService;
        }

        //GET api/correlatedSector
        [HttpGet]
        public IHttpActionResult Get([FromUri] CorrelatedSektorListParameter param)
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

                    var scneario = _scenarioService.GetDefault();
                    int totalRows = _correlatedSektorService.GetByScenarioDefaultId().Count();
                    var totalPage = totalRows / param.PageSize;
                    var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
                    if (totalPages < 0)
                        totalPages = 0;

                    var result = _correlatedSektorService.GetByScenarioDefaultId()
                        .Skip(skip)
                        .Take(param.PageSize)
                        .ToList();

                    var riskRegistrasi = _riskRegistrasiService.GetAll().ToList();
                    var correlationMatrix = _correlationMatrixService.GetAll().ToList();
                    IList<CorrelatedSektorDTO> colls = CorrelatedSektorDTO.From(result, riskRegistrasi, correlationMatrix);

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

        //GET api/correlatedSector/1
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var riskRegistrasi = _riskRegistrasiService.GetAll().ToList();
                var correlationMatrix = _correlationMatrixService.GetAll().ToList();
                var result = _correlatedSektorService.Get(id);
                CorrelatedSektorDTO correlatedSectorDTO = CorrelatedSektorDTO.From(result, riskRegistrasi, correlationMatrix);
                return Ok(correlatedSectorDTO);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //POST api/correlatedSector
        [HttpPost]
        public IHttpActionResult Add(CorrelatedSektorParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _correlatedSektorService.Add(param);
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

        //PUT api/correlatedSector/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]CorrelatedSektorParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.UpdateDate = getDate;
                    param.UpdateBy = userId;

                    int result = _correlatedSektorService.Update(id, param);
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

        //DELETE api/correlatedSector/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();
                    int result = _correlatedSektorService.Delete(id, userId, getDate);
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
