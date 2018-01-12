using KW.Core;
using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application
{
    public class UserResetPasswordService : IUserResetPasswordService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserResetPasswordRepository _userResetPasswordRepository;
        private readonly IEmailService _emailService;

        public UserResetPasswordService(IUnitOfWork uow, IUserResetPasswordRepository userResetPasswordRepository, IEmailService emailService)
        {
            _uow = uow;
            _userResetPasswordRepository = userResetPasswordRepository;
            _emailService = emailService;
        }

        public void ResetPassword(string userName, string url)
        {
            UserResetPassword resetPass = new UserResetPassword(userName);

            using (_uow)
            {
                _userResetPasswordRepository.Insert(resetPass);
                _uow.Commit();
            }

            _emailService.NotifyResetPassword(userName, resetPass.RequestToken, url);
        }
    }
}
