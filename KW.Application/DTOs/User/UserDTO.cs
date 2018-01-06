using KW.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KW.Domain;
using KW.Common;

namespace KW.Application.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string Language { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public string ExpiryDate { get; set; }
        public IList<RoleDTO> Role { get; set; }

        public UserDTO()
        {
        }
        public UserDTO(User user)
        {
            if (user == null) return;

            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Password = user.Password;
            this.Status = user.Status;
            this.Language = user.Language;
            this.CreateDate = user.CreateDate;
            this.CreateTime = user.CreateTime;
            this.ExpiryDate = user.ExpiryDate;
            if (user.UserRoles != null && user.UserRoles.Count > 0)
                this.Role = user.UserRoles.Select(x => new RoleDTO(x.Role)).ToList();
            else
            {
                this.Role = new List<RoleDTO>();
                this.Role.Add(new RoleDTO() { Id = 0, Name = "", Description = "" });
            }
        }

        public static UserDTO From(User user)
        {
            return new UserDTO(user);
        }

        public static IList<UserDTO> From(IList<User> users)
        {
            IList<UserDTO> userDTO = new List<UserDTO>();
            foreach (var user in users)
            {
                userDTO.Add(new UserDTO(user));
            }
            return userDTO;
        }

    }

    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
        public string Language { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public string ExpiryDate { get; set; }
        public IList<RoleDTO> Role { get; set; }
        //public int? EmployeeId { get; set; }
        public UserDetailDTO()
        {
        }

        //public UserDetailDTO(User user)
        public UserDetailDTO(User user)
        {
            if (user == null) return;

            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Status = user.Status;
            this.Language = user.Language;
            this.CreateDate = user.CreateDate;
            this.CreateTime = user.CreateTime;
            this.ExpiryDate = user.ExpiryDate;
            if (user.UserRoles != null && user.UserRoles.Count > 0)
                this.Role = user.UserRoles.Select(x => new RoleDTO(x.Role)).ToList();
            else
            {
                this.Role = new List<RoleDTO>();
                this.Role.Add(new RoleDTO() { Id = 0, Name = "", Description = "" });
            }
        }

        public static UserDetailDTO From(User user)
        {
            return new UserDetailDTO(user);
        }
    }


    public class UserDetailMaskingDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
        public string Language { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public string ExpiryDate { get; set; }
        public IList<RoleDTO> Role { get; set; }
        public UserDetailMaskingDTO()
        {
        }
        //public UserDetailDTO(User user)
        public UserDetailMaskingDTO(User user)
        {
            if (user == null) return;

            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Status = user.Status;
            this.Language = user.Language;
            this.CreateDate = user.CreateDate;
            this.CreateTime = user.CreateTime;
            this.ExpiryDate = user.ExpiryDate;

            //this.MaskingField = maskingField.Select(x => new List<MaskingFieldsSettingDTO>(x.FieldName)).

            if (user.UserRoles != null && user.UserRoles.Count > 0)
                this.Role = user.UserRoles.Select(x => new RoleDTO(x.Role)).ToList();
            else
            {
                this.Role = new List<RoleDTO>();
                this.Role.Add(new RoleDTO() { Id = 0, Name = "", Description = "" });
            }
        }

        public static UserDetailMaskingDTO From(User user)
        {
            return new UserDetailMaskingDTO(user);
        }
    }

    public class UserListDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
        public string Language { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public string ExpiryDate { get; set; }
        public IList<RoleListDTO> Role { get; set; }

        public UserListDTO()
        {
        }
        public UserListDTO(User user)
        {
            if (user == null) return;

            this.Id = user.Id;
            this.FullName = user.UserName;
            this.UserName = user.UserName;
            this.Status = user.Status;
            this.Language = user.Language;
            this.CreateDate = user.CreateDate;
            this.CreateTime = user.CreateTime;
            this.ExpiryDate = user.ExpiryDate;
            if (user.UserRoles != null && user.UserRoles.Count > 0)
            {
                this.Role = user.UserRoles.Select(x => new RoleListDTO(x.Role)).ToList();
            }
            else
            {
                this.Role = new List<RoleListDTO>();
                this.Role.Add(new RoleListDTO() { Id = 0, Name = "", Description = "" });
            }
        }

        public static UserListDTO From(User user)
        {
            return new UserListDTO(user);
        }

        public static IList<UserListDTO> From(IList<User> users)
        {
            IList<UserListDTO> userDTO = new List<UserListDTO>();
            foreach (var user in users)
            {
                userDTO.Add(new UserListDTO(user));
            }
            return userDTO;
        }
    }

    public class UserLiteDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<MenuAccessLiteWithChildDTO> MenuList { get; set; }
        public string RoleName { get; set; }
        public IList<int> EmployeeType { get; set; }
        public UserLiteDTO()
        {
        }
    }
}
