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
    public class SektorController : BaseAPIController
    {
        private readonly ISektorService _sektorService;

        public SektorController(ISektorService sektorService)
        {
            _sektorService = sektorService;
        }

        //GET api/sektor
        [HttpGet]
        public IHttpActionResult Get([FromUri] SektorListParameter param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IList<Sektor> sektors = _sektorService.GetAll().ToList();
                    if(param.IsPagination())
                    {
                        string keyword = string.Empty;
                        string field = string.Empty;

                        param.Validate();
                        keyword = param.Search;
                        field = param.SearchBy;

                        int skip = 0;
                        if (param.PageNo > 0)
                        {
                            skip = (param.PageNo -1) * param.PageSize;
                        }

                        int totalRows = _sektorService.GetAll().Count();
                        var totalPage = totalRows / param.PageSize;
                        var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
                        if (totalPages < 0)
                            totalPages = 0;

                        var result = sektors.Skip(skip).Take(param.PageSize).ToList();

                        IList<SektorDTO> colls = SektorDTO.From(result);

                        PaginationDTO page = new PaginationDTO();
                        page.PageCount = totalPages;
                        page.PageNo = param.PageNo;
                        page.PageSize = param.PageSize;
                        page.results = colls;

                        return Ok(page);
                    }
                    else
                    {
                        IList<SektorDTO> dto = SektorDTO.From(sektors);
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
                var result = _sektorService.Get(id);
                SektorDTO sektorDTO = SektorDTO.From(result);
                return Ok(sektorDTO);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //POST api/sektor
        [HttpPost]
        public IHttpActionResult Add(SektorParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _sektorService.Add(param);
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

        //PUT api/sektor/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]SektorParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.UpdateDate = getDate;
                    param.UpdateBy = userId;

                    int result = _sektorService.Update(id, param);
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

        //DELETE api/sektor/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();
                    int result = _sektorService.Delete(id, userId, getDate);
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
