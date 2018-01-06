using KW.Application;
using KW.Application.DTO;
using KW.Application.Params;
using KW.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace KW.Presentation.WebAPI.Controllers
{
    public class CorrelationRiskAntarSektorController : BaseAPIController
    {
        private readonly ICorrelationRiskAntarSektorService _correlationRiskAntarSektor;
        private readonly IScenarioService _scenarioiSektor;
        private readonly ICorrelationMatrixService _correlationMatrixService;

        public CorrelationRiskAntarSektorController(ICorrelationRiskAntarSektorService correlationRiskAntarSektor, IScenarioService scenarioiSektor, ICorrelationMatrixService correlationMatrixService)
        {
            _correlationRiskAntarSektor = correlationRiskAntarSektor;
            _scenarioiSektor = scenarioiSektor;
            _correlationMatrixService = correlationMatrixService;
        }

        //GET api/CorrelationRiskAntarSector
        [HttpGet]
        public IHttpActionResult Get([FromUri] CorrelationRiskAntarSektorListParameter param)
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

                    var scenarioDefault = _scenarioiSektor.GetDefault();
                    int totalRows = _correlationRiskAntarSektor.GetByScenarioDefault(scenarioDefault.Id).Count();
                    var totalPage = totalRows / param.PageSize;
                    var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
                    if (totalPages < 0)
                        totalPages = 0;

                    var result = _correlationRiskAntarSektor.GetByScenarioDefault(scenarioDefault.Id)
                        .Skip(skip)
                        .Take(param.PageSize)
                        .ToList();

                    var correlationMatrix = _correlationMatrixService.GetAll().ToList();
                    IList<CorrelationRiskAntarSektorDTO> colls = CorrelationRiskAntarSektorDTO.From(result, correlationMatrix);

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

        //GET api/CorrelationRiskAntarSector/id
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _correlationRiskAntarSektor.Get(id);
                var correlationMatrix = _correlationMatrixService.GetAll().ToList();

                CorrelationRiskAntarSektorDTO correlationDTO = CorrelationRiskAntarSektorDTO.From(result, correlationMatrix);
                return Ok(correlationDTO);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //POST api/CorrelationRiskAntarSector
        [HttpPost]
        public IHttpActionResult Add(CorrelationRiskAntarSektorParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _correlationRiskAntarSektor.Add(param);
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

        //PUT api/CorrelationRiskAntarSector/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]CorrelationRiskAntarSektorParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.UpdateDate = getDate;
                    param.UpdateBy = userId;
                   
                    var result = _correlationRiskAntarSektor.Update(id, param);
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

        //DELETE api/CorrelationRiskAntarSector/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();
                    int result = _correlationRiskAntarSektor.Delete(id, userId, getDate);
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
