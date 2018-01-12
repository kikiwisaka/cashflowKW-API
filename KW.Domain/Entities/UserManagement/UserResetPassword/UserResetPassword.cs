using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class UserResetPassword
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string RequestToken { get; set; }
        public string RequestDate { get; set; }
        public string TimeRequest { get; set; }

        public UserResetPassword() { }

        public UserResetPassword(string userName)
        {
            this.UserName = userName;
            this.RequestToken = Guid.NewGuid().ToString();
            this.RequestDate = DateTime.Now.ToString("yyyyMMdd");
            this.TimeRequest = DateTime.Now.ToString("HHmm");
        }
    }
}
