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
    public class LikehoodDetailController : BaseAPIController
    {
        private readonly ILikehoodDetailService _likehoodDetailService;
        private readonly ILikehoodService _likehoodService;

        public LikehoodDetailController(ILikehoodDetailService likehoodDetailService, ILikehoodService likehoodService)
        {
            _likehoodDetailService = likehoodDetailService;
            _likehoodService = likehoodService;
        }

        //GET api/likehooddetail
        [HttpGet]
        public IHttpActionResult Get([FromUri] LikehoodDetailListParam param)
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

                    int totalRows = _likehoodDetailService.GetByLikehoodId(param.ParentId).Count();
                    var totalPage = totalRows / param.PageSize;
                    var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
                    if (totalPages < 0)
                        totalPages = 0;

                    var result = _likehoodDetailService.GetByLikehoodId(param.ParentId)
                        .Skip(skip)
                        .Take(param.PageSize)
                        .ToList();

                    var likehood = _likehoodService.Get(param.ParentId);
                    IList<LikehoodDetailDTO> colls = LikehoodDetailDTO.From(result, likehood);

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

        //GET api/likehooddetail/id
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _likehoodDetailService.Get(id);
                var likehood = _likehoodService.Get(result.LikehoodId);
                LikehoodDetailDTO likehoodDTO = LikehoodDetailDTO.From(result, likehood);
                return Ok(likehoodDTO);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //POST api/likehooddetail
        [HttpPost]
        public IHttpActionResult Add(LikehoodDetailParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _likehoodDetailService.Add(param);
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

        //PUT api/likehooddetail/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]LikehoodDetailParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.UpdateDate = getDate;
                    param.UpdateBy = userId;

                    int result = _likehoodDetailService.Update(id, param);

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
