using KW.Application;
using KW.Application.Params;
using KW.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace KW.Presentation.WebAPI.Controllers
{
    public class CorrelatedProjectDetailController : BaseAPIController
    {
        private readonly ICorrelatedProjectDetailService _correlatedProjectDetailService;

        public CorrelatedProjectDetailController(ICorrelatedProjectDetailService correlatedProjectDetailService)
        {
            _correlatedProjectDetailService = correlatedProjectDetailService;
        }

        //GET api/CorrelatedProjectDetail/1
        [HttpGet]
        public IHttpActionResult Get(int id) //id => correlatedProjectId
        {
            try
            {
                var result = _correlatedProjectDetailService.GetByCorrelatedProjectId(id);
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

        //POST api/CorrelatedProjectDetail
        [HttpPost]
        public IHttpActionResult Add(CorrelatedProjectDetailCollectionParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _correlatedProjectDetailService.Add(param);
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
