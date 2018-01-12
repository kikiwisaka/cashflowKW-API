using KW.Core;
using KW.Domain;
using System;
using System.Collections.Generic;
using KW.Application.DTO;

namespace KW.Application
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public void AssertIdNotAlreadyExist(string userName)
        {
            throw new NotImplementedException();
        }

        public void AssertIdNotAlreadyExistUpdate(int id, string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        public User GetByUserName(string userName)
        {
            return _userRepository.Find(userName);
        }

        public bool isExistUserName(string userName)
        {
            return _userRepository.isExist(userName);
        }

        public UserRoleDTO GetUserRole(int userId)
        {
            var result = _userRoleRepository.GetByUserId(userId);
            return UserRoleDTO.From(result);
        }
    }
}
