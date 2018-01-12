using KW.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string Language { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public string ExpiryDate { get; set; }
        public string LastUpdateDate { get; set; }

        public int? EmployeeId { get; set; }

        public virtual IList<UserRole> UserRoles { get; set; }

        public User()
        {
            this.Language = "en";
            this.UserRoles = new List<UserRole>();
        }
        public User(string userName, string password, bool status, string expiryDate, string date, string time) : this()
        {
            this.UserName = userName;
            this.Password = DataSecurity.Encrypt(password);
            this.CreateDate = date;
            this.CreateTime = time;
            this.Status = status;
            this.ExpiryDate = expiryDate;
        }

        public User(string userName, string password, bool status, IList<UserRole> UserRole, string date, string time)
            : this(userName, password, status, "", date, time)
        {
            this.UserRoles = UserRole;
            //this.Accesses = userAccess;
        }

        public User(string userName, string password, bool status, bool isSenior,string date, string time)
            : this(userName, password, status, "", date, time)
        {
            
        }

        public void UpdateUserRole(UserRole UserRole)
        {
            this.UserRoles[0] = UserRole;
        }

        public User(string userName, string password, bool status, bool isSenior, IList<UserRole> UserRole, string date, string time)
            : this(userName, password, status, UserRole, date, time)
        {
        }

        public void Update(string userName, string password, bool status, string expiryDate = "")
        {
            this.UserName = userName;
            this.Password = DataSecurity.Encrypt(password);
            this.Status = status;
            this.ExpiryDate = expiryDate;
           
        }


        public void ChangePassword(string oldPassword, string newPassword)
        {
            //Validate.IsOldPasswordValid(Security.Decrypt(this.password), oldPassword);
            this.Password = DataSecurity.Encrypt(newPassword);
        }

        public void ChangeLanguage(string lang)
        {
            this.Language = lang;
        }

        public bool ValidatePassword(string pass)
        {
            string confirmPass = DataSecurity.Decrypt(this.Password);
            return pass == confirmPass;
        }

        public bool IsActive()
        {
            return this.Status;
        }

        public void Activate()
        {
            this.Status = true;
        }
        public void Deactive()
        {
            this.Status = false;
        }
        public string EncodePassword(string password)
        {
            return DataSecurity.Encrypt(password).ToString();
        }
        public void AddUserRole(UserRole UserRole)
        {
            this.UserRoles.Add(UserRole);
        }
        public void RemoveUserRole(UserRole UserRole)
        {
            this.UserRoles.Remove(UserRole);
        }
    }
}
