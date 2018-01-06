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
    public class ScenarioController : BaseAPIController
    {
        private readonly IScenarioService _scenarioService;

        public ScenarioController(IScenarioService scenarioService)
        {
            _scenarioService = scenarioService;
        }

        //GET api/scenario
        [HttpGet]
        public IHttpActionResult Get([FromUri] ScenarioListParameter param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IList<Scenario> scenarios = _scenarioService.GetAll().ToList();
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

                        int totalRows = _scenarioService.GetAll().Count();
                        var totalPage = totalRows / param.PageSize;
                        var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
                        if (totalPages < 0)
                            totalPages = 0;

                        var result = _scenarioService.GetAll()
                            .Skip(skip)
                            .Take(param.PageSize)
                            .ToList();

                        IList<ScenarioDTO> colls = ScenarioDTO.From(result);

                        PaginationDTO page = new PaginationDTO();
                        page.PageCount = totalPages;
                        page.PageNo = param.PageNo;
                        page.PageSize = param.PageSize;
                        page.results = colls;

                        return Ok(page);
                    }
                    else
                    {
                        IList<ScenarioDTO> dto = ScenarioDTO.From(scenarios);
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

        //GET api/scenario/id
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _scenarioService.Get(id);
                ScenarioDTO stageDTO = ScenarioDTO.From(result);
                return Ok(stageDTO);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //POST api/scenario
        [HttpPost]
        public IHttpActionResult Add(ScenarioParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _scenarioService.Add(param);
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

        //PUT api/scenario/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]ScenarioParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.UpdateDate = getDate;
                    param.UpdateBy = userId;

                    int result;
                    if (param.IsDefault == true)
                    {
                        result = _scenarioService.SetDefault(id, param.UpdateBy, param.UpdateDate);
                    }
                    else if (param.IsUpdate == true)
                    {
                        result = _scenarioService.Update(id, param);
                    }
                    else
                    {
                        result = _scenarioService.Update(id, param);
                    }
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

        //DELETE api/scenario/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();
                    int result = _scenarioService.Delete(id, userId, getDate);
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
