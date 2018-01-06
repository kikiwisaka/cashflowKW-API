using KW.Application.DTO;
using KW.Common;
using KW.Domain;
using KW.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KW.Presentation.WebAPI
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private string _clientid { get; set; }
        private string _username { get; set; }
        private string _ip { get; set; }
        public OAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); //
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            DatabaseContext dbContext = new DatabaseContext();

            string userName = context.UserName;
            string password = context.Password;

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                context.SetError("invalid_grant", "Invalid credentials");
                return;
            }

            User user = dbContext.Users.Where(x => x.UserName == context.UserName).SingleOrDefault();


            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (user != null)
            {
                UserDTO userDTO = UserDTO.From(user);
                string existingPassword = DataSecurity.Decrypt(user.Password);
                if(password != existingPassword)
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    return;
                }

                UserRole userRole = dbContext.UserRoles.Where(x => x.UserId == user.Id).SingleOrDefault();
                if(userRole != null)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, userRole.Role.Name));
                    identity.AddClaim(new Claim("username", user.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.UserData, user.Id.ToString()));
                    identity.AddClaim(new Claim("userId", user.Id.ToString()));

                    var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {
                            "username", userName
                        },
                        {
                            "role", userRole.Role.Name
                        },
                        {
                            "id", userDTO.Id.ToString()
                        }
                    });


                    if (userDTO.Role != null && userDTO.Role.Count > 0)
                    {
                        foreach (RoleDTO role in userDTO.Role)
                        {
                            if (role.Accesses != null && role.Accesses.Count > 0)
                            {
                                List<MenuDTO> menuList = role.Accesses.ToList();

                                foreach (MenuDTO menu in menuList)
                                {
                                    identity.AddClaim(new Claim(ClaimTypes.Webpage, menu.ControllerName + "$%" + menu.ActionName));
                                }
                            }
                        }
                    }


                    string urlAPI = string.Format("api/Menu/GetByGeneralAccess?generalAccess=1");
                    var generlAccessMenu = dbContext.Menus.Where(x => x.IsGeneralAccess == true).ToList();
                    if (generlAccessMenu.Count > 0)
                    {
                        IList<MenuDTO> generlAccessMenuDTO = MenuDTO.From(generlAccessMenu);
                        if (generlAccessMenuDTO != null && generlAccessMenuDTO.Count > 0)
                        {
                            foreach (MenuDTO menu in generlAccessMenuDTO)
                            {
                                identity.AddClaim(new Claim(ClaimTypes.Webpage, menu.ControllerName + "$%" + menu.ActionName));
                            }
                        }
                    }

                    var ticket = new AuthenticationTicket(identity, props);

                    context.Validated(ticket);
                    context.Request.Context.Authentication.SignIn(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Existing user not set any Role(s)");
                    return;
                }
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
        }
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}