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
    public class StageTahunRiskMatrixDetailController : BaseAPIController
    {
        private readonly IStageTahunRiskMatrixDetailService _stageTahunRiskMatrixDetailService;
        public StageTahunRiskMatrixDetailController(IStageTahunRiskMatrixDetailService stageTahunRiskMatrixDetailService)
        {
            _stageTahunRiskMatrixDetailService = stageTahunRiskMatrixDetailService;
        }

        //GET api/stageTahunRiskMatrixDetail
        //[HttpGet]
        //public IHttpActionResult Get([FromUri] StageTahunRiskMatrixDetailListParam param)
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
        //            if (param.PageNo > 1)
        //            {
        //                skip = param.PageNo * param.PageSize;
        //            }

        //            int totalRows = _stageTahunRiskMatrixDetailService.GetByStageTahunRiskMatrixId(param.ParentId).Count();
        //            var totalPage = totalRows / param.PageSize;
        //            var totalPages = (int)Math.Ceiling((double)totalRows / param.PageSize) - 1;
        //            if (totalPages < 0)
        //                totalPages = 0;

        //            var result = _stageTahunRiskMatrixDetailService.GetByStageTahunRiskMatrixId(param.ParentId)
        //                .Skip(skip)
        //                .Take(param.PageSize)
        //                .ToList();

        //            IList<StageTahunRiskMatrixDetailDTO> colls = StageTahunRiskMatrixDetailDTO.From(result);

        //            PaginationDTO page = new PaginationDTO();
        //            page.PageCount = totalPages;
        //            page.PageNo = param.PageNo;
        //            page.PageSize = param.PageSize;
        //            page.results = colls;

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

        //GET api/stageTahunRiskMatrixDetail/1
        [HttpGet]
        public IHttpActionResult Get(int id) //id = RiskMatrixProjectId
        {
            try
            {
                var result = _stageTahunRiskMatrixDetailService.GetByRiskMatrixProjectId(id);
                //IList<StageTahunRiskMatrixDetailDTO> stageTahunRiskMatrixDetailDTO = StageTahunRiskMatrixDetailDTO.From(result);
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

        //POST api/stageTahunRiskMatrixDetail
        [HttpPost]
        public IHttpActionResult Add(RiskMatrixCollectionParameter param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _stageTahunRiskMatrixDetailService.Add(param);
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

        //PUT api/stageTahunRiskMatrixDetail/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]RiskMatrixCollectionParameter param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.UpdateDate = getDate;
                    param.UpdateBy = userId;

                    int result = _stageTahunRiskMatrixDetailService.Update(id, param);
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

        //DELETE api/stageTahunRiskMatrixDetail/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();
                    int result = _stageTahunRiskMatrixDetailService.Delete(id, userId, getDate);
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
