using KW.Domain;
using KW.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KW.Presentation.WebAPI.Controllers
{
    public class DashboardRiskCapitalProjectController : BaseAPIController
    {
        private readonly IDashboardRiskCapitalProjectService _dashboardRiskCapitalProjectService;

        public DashboardRiskCapitalProjectController(IDashboardRiskCapitalProjectService dashboardRiskCapitalProjectService)
        {
            _dashboardRiskCapitalProjectService = dashboardRiskCapitalProjectService;
        }

        [HttpGet]
        public IHttpActionResult GetDashboardDiversifiedRiskCapitalProject(bool isDivesified)
        {
            try
            {
                var xxx = _dashboardRiskCapitalProjectService.GetDiversifedRiskCapitalProject();
                return Ok("sa");
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
