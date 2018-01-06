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
    public class OverAllCommentsController : BaseAPIController
    {
        private readonly IOverAllCommentsService _overAllCommentsService;

        public OverAllCommentsController(IOverAllCommentsService overAllCommentsService)
        {
            _overAllCommentsService = overAllCommentsService;
        }

        //GET api/comments
        //[HttpGet]
        //public IHttpActionResult Get([FromUri] OverAllCommentsListParameter param)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            string keyword = string.Empty;
        //            string field = string.Empty;

        //            param.Validate();
        //            keyword = param.Search;
        //            field = param.SearchBy;

        //            int skip = 0;
        //            if (param.PageNo > 0)
        //            {
        //                skip = (param.PageNo - 1) * param.PageSize;
        //            }

        //            int totalRows = _overAllCommentsService.GetByColorId(param.ParentId).Count();
        //            var totalPage = totalRows / param.PageSize;
        //            var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize);
        //            if (totalPages < 0)
        //                totalPages = 0;

        //            var result = _overAllCommentsService.GetByColorId(param.ParentId)
        //                .Skip(skip)
        //                .Take(param.PageSize)
        //                .ToList().Last();

        //            //IList<OverAllCommentsDTO> colls = OverAllCommentsDTO.From(result);

        //            //PaginationDTO page = new PaginationDTO();
        //            //page.PageCount = totalPages;
        //            //page.PageNo = param.PageNo;
        //            //page.PageSize = param.PageSize;
        //            //page.results = colls;

        //            return Ok(result);
        //        }
        //        else
        //        {
        //            string errorResult = string.Join(" ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
        //            return Content(HttpStatusCode.BadRequest, errorResult);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException == null)
        //            return Content(HttpStatusCode.InternalServerError, ex.Message);
        //        else
        //            return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
        //    }
        //}

        //GET api/comments/1
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _overAllCommentsService.GetByColorId(id).ToList().Last();
                OverAllCommentsDTO commentsDTO = OverAllCommentsDTO.From(result);
                return Ok(commentsDTO);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                else
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        //GET api/comment?colorId={id}
        //[HttpGet]
        //public IHttpActionResult GetByRiskId(int colorId, int PageNo, int PageSize)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            int skip = 0;
        //            if (PageNo > 1)
        //            {
        //                skip = PageNo * PageSize;
        //            }

        //            var result = _overAllCommentsService.GetByColorId(colorId).ToList();

        //            int totalRows = result.Count();
        //            var totalPage = totalRows / PageSize;
        //            var totalPages = (int)Math.Ceiling((double)totalRows / PageSize) - 1;
        //            if (totalPages < 0)
        //                totalPages = 0;

        //            IList<OverAllCommentsDTO> commentsDTO = OverAllCommentsDTO.From(result);

        //            PaginationDTO page = new PaginationDTO();
        //            page.PageCount = totalPages;
        //            page.PageNo = PageNo;
        //            page.PageSize = PageSize;
        //            page.results = commentsDTO;

        //            return Ok(page);
        //        }
        //        else
        //        {
        //            string errorResult = string.Join(" ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
        //            return Content(HttpStatusCode.BadRequest, errorResult);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException == null)
        //            return Content(HttpStatusCode.InternalServerError, ex.Message);
        //        else
        //            return Content(HttpStatusCode.InternalServerError, ex.InnerException.Message);
        //    }
        //}

        //POST api/comments
        [HttpPost]
        public IHttpActionResult Add(OverAllCommentsParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _overAllCommentsService.Add(param);
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

        //PUT api/comments/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]OverAllCommentsParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.UpdateDate = getDate;
                    param.UpdateBy = userId;

                    int result = _overAllCommentsService.Update(id, param);
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

        //DELETE api/comments/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();
                    int result = _overAllCommentsService.Delete(id, userId, getDate);
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
