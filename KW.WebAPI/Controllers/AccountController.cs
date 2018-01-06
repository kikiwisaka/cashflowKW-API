using KW.Application;
using KW.Application.DTO;
using KW.Application.Params;
using KW.Presentation.WebAPI.Helpers;
using KW.Presentation.WebAPI.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace KW.Presentation.WebAPI.Controllers
{
    public class AccountController : BaseAPIController
    {
        private IMenuService _menuService;
        private IUserService _userService;
        private IUserResetPasswordService _userResetPasswordService;
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        public AccountController(IMenuService menuService, IUserService userService, IUserResetPasswordService userResetPasswordService)
        {
            _menuService = menuService;
            _userService = userService;
            _userResetPasswordService = userResetPasswordService;
        }

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("api/values/getall")]
        //public IHttpActionResult Get()
        //{
        //    return Ok("Now server time is: " + DateTime.Now.ToString());
        //}

        //[Authorize(Roles = CustomeRoles.Admin)]
        //[HttpGet]
        //[Route("api/values/GetForAdmin")]
        //public IHttpActionResult GetForAdmin()
        //{
        //    var identity = (ClaimsIdentity)User.Identity;
        //    var roles = identity.Claims
        //                .Where(c => c.Type == ClaimTypes.Role)
        //                .Select(c => c.Value);
        //    return Ok("Hello " + identity.Name + " Role: " + string.Join(",", roles.ToList()));
        //}

        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Content(HttpStatusCode.OK, "Success");
        }

        [Route("api/Account/ResetPassword")]
        public IHttpActionResult ResetPassword(string userName)
        {
            try
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                if (ModelState.IsValid)
                {
                    result.Add("UserName", userName);
                    result.Add("Status", "OK");

                    if (!string.IsNullOrEmpty(userName) && _userService.isExistUserName(userName))
                    {
                        var url = "http://" + HttpContext.Current.Request.Url.Authority.ToString();
                        _userResetPasswordService.ResetPassword(userName, url);
                        return Ok(result);
                    }
                    else
                        return Ok(result);
                }
                else
                {
                    result.Add("UserName", userName);
                    result.Add("Status", "Invalid");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                string errMessage = string.Empty;
                if (!string.IsNullOrWhiteSpace(ex.InnerException.Message))
                    errMessage = ex.InnerException.Message;
                else
                    errMessage = ex.Message;

                return Content(HttpStatusCode.InternalServerError, errMessage);
            }
        }

        [Route("api/Account/GetUserData")]

        public IHttpActionResult GetCurrentUserLiteData()
        {
            try
            {
                UserLiteDTO data = new UserLiteDTO();
                List<Claim> claims = ClaimsPrincipal.Current.Claims.ToList();
                if(claims.Count < 1)
                {
                    return Content(HttpStatusCode.Unauthorized, "Access is denied due to invalid credentials");
                }
                data.UserName = claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
                data.Id = Convert.ToInt32(claims.Where(x => x.Type == ClaimTypes.UserData).FirstOrDefault().Value);
                List<MenuAccessLiteParameters> list = new List<MenuAccessLiteParameters>();
                List<string> listAccess = claims.Where(x => x.Type == ClaimTypes.Webpage).Select(y => y.Value).ToList();
                foreach (string access in listAccess)
                {
                    string[] acc = access.Split(new string[] { "$%" }, StringSplitOptions.None);
                    if (acc != null && acc.Count() == 2)
                    {
                        MenuAccessLiteParameters menu = new MenuAccessLiteParameters();
                        menu.ControllerName = acc[0];
                        menu.ActionName = acc[1];
                        list.Add(menu);
                    }
                }

                IList<MenuAccessLiteWithChildDTO> resListAccess;

                resListAccess = _menuService.GetByListControllerAndActionMenu(list);

                if (resListAccess != null)
                {
                    var userRole = _userService.GetUserRole(data.Id);

                    if (userRole != null)
                    {
                        data.RoleName = userRole.Role.Name;
                        data.EmployeeType = userRole.Role.EmployeeTypes;
                        if (userRole.Role.Name == "SuperAdmin")
                        {
                            data.MenuList = resListAccess.ToList();
                        }
                        else
                        {
                            var su = new SuperUserHelper();
                            data.MenuList = su.filterSuperAdminAccess(resListAccess).ToList();
                        }
                    }
                    else
                    {
                        data.MenuList = null;
                    }
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                string errMessage = string.Empty;
                if (!string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    errMessage = ex.InnerException.Message;
                }else
                {
                    errMessage = ex.Message;
                }

                return Content(HttpStatusCode.InternalServerError, errMessage);
            }
        }

        public class SuperUserHelper
        {
            public IList<MenuAccessLiteWithChildDTO> filterSuperAdminAccess(IList<MenuAccessLiteWithChildDTO> resListAccess)
            {
                for (int i = 0; i < resListAccess.Count(); i++)
                {
                    if (!resListAccess[i].IsSuperAdminOnly)
                    {
                        if (resListAccess[i].Children.Count > 0)
                        {
                            foreach (var item in resListAccess[i].Children)
                            {
                                if (!item.IsSuperAdminOnly && item.Children.Count > 0)
                                {
                                    foreach (var child in item.Children)
                                    {
                                        IList<MenuAccessLiteWithChildDTO> newChild2 = item.Children.Where(x => x.IsSuperAdminOnly == false).ToList();
                                        item.Children = null;
                                        item.Children = MenuAccessLiteWithChildDTO.From(newChild2).ToList();
                                    }
                                }
                            };
                            IList<MenuAccessLiteWithChildDTO> newChild = resListAccess[i].Children.Where(x => x.IsSuperAdminOnly == false).ToList();
                            resListAccess[i].Children = null;
                            resListAccess[i].Children = MenuAccessLiteWithChildDTO.From(newChild).ToList();
                        }
                    }
                };

                IList<MenuAccessLiteWithChildDTO> newResListAccess = resListAccess.Where(x => x.IsSuperAdminOnly == false).ToList();
                return newResListAccess;
            }
        }
    }
}
