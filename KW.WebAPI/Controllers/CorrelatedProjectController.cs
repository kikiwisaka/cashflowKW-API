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
    public class CorrelatedProjectController : BaseAPIController
    {
        private readonly ICorrelatedProjectService _correlatedProjectService;
        private readonly ICorrelationRiskAntarSektorService _correlationRiskAntarSektorService;
        private readonly ICorrelationMatrixService _correlationMatrixService;
        private readonly IScenarioService _scenarioService;

        public CorrelatedProjectController(ICorrelatedProjectService correlatedProjectService, ICorrelationRiskAntarSektorService correlationRiskAntarSektorService, ICorrelationMatrixService correlationMatrixService,
            IScenarioService scenarioService)
        {
            _correlatedProjectService = correlatedProjectService;
            _correlationRiskAntarSektorService = correlationRiskAntarSektorService;
            _correlationMatrixService = correlationMatrixService;
            _scenarioService = scenarioService;
        }

        //GET api/correlatedSector
        [HttpGet]
        public IHttpActionResult Get([FromUri] CorrelatedProjectListParameter param)
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

                    int totalRows = _correlatedProjectService.GetByScenarioDefaultId().Count();
                    var totalPage = totalRows / param.PageSize;
                    var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
                    if (totalPages < 0)
                        totalPages = 0;

                    var result = _correlatedProjectService.GetByScenarioDefaultId()
                        .Skip(skip)
                        .Take(param.PageSize)
                        .ToList();

                    var scenarioDefault = _scenarioService.GetDefault();
                    var projects = _correlationRiskAntarSektorService.GetProjectByScenarioDefault(scenarioDefault.Id).ToList();
                    var correlationMatrix = _correlationMatrixService.GetAll().ToList();
                    IList<CorrelatedProjectDTO> colls = CorrelatedProjectDTO.From(result, correlationMatrix, projects);

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

        //GET api/correlatedProject/1
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var scenarioDefault = _scenarioService.GetDefault();
                var projects = _correlationRiskAntarSektorService.GetProjectByScenarioDefault(scenarioDefault.Id).ToList();
                var correlationMatrix = _correlationMatrixService.GetAll().ToList();
                var result = _correlatedProjectService.Get(id);
                CorrelatedProjectDTO dto = CorrelatedProjectDTO.From(result, correlationMatrix, projects);
                return Ok(dto);
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
