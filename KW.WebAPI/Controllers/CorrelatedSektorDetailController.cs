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
    public class CorrelatedSektorDetailController : BaseAPIController
    {
        private readonly ICorrelatedSektorDetailService _correlatedSektorDetailService;
        private readonly IRiskRegistrasiService _riskRegistrasiService;
        public CorrelatedSektorDetailController(ICorrelatedSektorDetailService correlatedSektorDetailService, IRiskRegistrasiService riskRegistrasiService)
        {
            _correlatedSektorDetailService = correlatedSektorDetailService;
            _riskRegistrasiService = riskRegistrasiService;
        }

        //GET api/CorrelatedSektorDetail/1
        [HttpGet]
        public IHttpActionResult Get(int id) //id => correlatedRiskAntarSektorId
        {
            try
            {
                var result = _correlatedSektorDetailService.GetByCorrelatedSektorId(id);
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

        //POST api/correlatedSektorDetail
        [HttpPost]
        public IHttpActionResult Add(CorrelatedSektorDetailCollectionParam param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = UserHelper.GetCurrentUserId();
                    DateTime getDate = DateHelper.GetDateTime();

                    param.CreateDate = getDate;
                    param.CreateBy = userId;

                    int id = _correlatedSektorDetailService.Add(param);
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
