using KW.Application;
using KW.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KW.Presentation.WebAPI.Helpers
{
    public class SessionHelper
    {
        private static IUserService _userService;
        public SessionHelper(IUserService userService)
        {
            _userService = userService;
        }
        public static UserDTO GetCurrentUser()
        {
            string username = HttpContext.Current.Session["Email"].ToString();
            UserDTO user = UserDTO.From(_userService.GetByUserName(username));
            return user;
        }
    }
}