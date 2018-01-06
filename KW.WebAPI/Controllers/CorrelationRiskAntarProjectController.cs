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
    public class CorrelationRiskAntarProjectController : BaseAPIController
    {
        private readonly ICorrelationRiskAntarProjectService _correlationRiskAntarProjectService;
        private readonly ICorrelationRiskAntarSektorService _correlationRiskAntarSektorService;
        private readonly IScenarioService _scenarioService;
        private readonly ICorrelationMatrixService _correlationMatrixService;
        private readonly IRiskRegistrasiService _riskRegistrasiService;

        public CorrelationRiskAntarProjectController(ICorrelationRiskAntarProjectService correlationRiskAntarProjectService, ICorrelationRiskAntarSektorService correlationRiskAntarSektorService, IScenarioService scenarioService,
            ICorrelationMatrixService correlationMatrixService, IRiskRegistrasiService riskRegistrasiService)
        {
            _correlationRiskAntarProjectService = correlationRiskAntarProjectService;
            _correlationRiskAntarSektorService = correlationRiskAntarSektorService;
            _scenarioService = scenarioService;
            _correlationMatrixService = correlationMatrixService;
            _riskRegistrasiService = riskRegistrasiService;
        }

        //GET api/CorrelationRiskAntarProject
        [HttpGet]
        public IHttpActionResult Get([FromUri] CorrelationRiskAntarProjectListParameter param)
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
                    int totalRows = _correlationRiskAntarSektorService.GetByScenarioDefault(scneario.Id).Count();
                    var totalPage = totalRows / param.PageSize;
                    var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
                    if (totalPages < 0)
                        totalPages = 0;

                    var result = _correlationRiskAntarSektorService.GetByScenarioDefault(scneario.Id)
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

        //GET api/CorrelationRiskAntarProject/id
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _correlationRiskAntarProjectService.GetByCorrelationRiskAntarSektorId(id);
                var correlationMatrix = _correlationMatrixService.GetAll().ToList();

                //CorrelationRiskAntarProjectDTO correlationDTO = CorrelationRiskAntarProjectDTO.From(result);
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
        //POST api/correlationRiskAntarProject
        [HttpPost]
        public IHttpActionResult Add(CorrelationRiskAntarProjectParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _correlationRiskAntarProjectService.Add(param);
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
    }
}
