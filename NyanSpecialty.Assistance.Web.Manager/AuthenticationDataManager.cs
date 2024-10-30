using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
namespace NyanSpecialty.Assistance.Web.Manager
{
    public class AuthenticationDataManager : IAuthenticationDataManager
    {

        private readonly ApplicationDBContext _dbContext;

        string _tokenKey = "oHpl2IxYiCpxriJpwb5xJC0ZwMrAUfhf0Y6rhsIVKo3Y8rgF9";

        public AuthenticationDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthResponse> AuthenticateUserAsync(UserAuthentication authentication)
        {
            AuthResponse authResponse = null;

            if (authentication != null)
            {
                if (!string.IsNullOrEmpty(authentication.username))
                {
                    var user = await _dbContext.users.Where(x => x.Email.ToLower().Trim() == authentication.username.ToLower().Trim() || x.Phone.Trim() == authentication.username.Trim()).FirstOrDefaultAsync();

                    if (user != null)
                    {
                        if (user.IsActive)
                        {
                            if (user.IsBlocked.HasValue && user.IsBlocked.Value)
                            {
                                authResponse = new AuthResponse()
                                {
                                    IsActive = true,
                                    JwtToken = "",
                                    StatusCode = (int)AuthenticationStatusCodes.userblocked,
                                    StatusMessage = "user blocked contact admin",
                                    ValidPassword = false,
                                    ValidUser = true
                                };
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(authentication.password))
                                {
                                    var validPassword = PasswordManagerUtility.VerifyPassword(authentication.password, user.PasswordHash, user.PasswordSalt);

                                    if (validPassword)
                                    {

                                        var tokenHandler = new JwtSecurityTokenHandler();

                                        var tokenKey = Encoding.ASCII.GetBytes(_tokenKey);

                                        var tokenDescrptor = new SecurityTokenDescriptor
                                        {
                                            Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                                            {
                                                new Claim(ClaimTypes.Name,authentication.username),
                                                new Claim("UserId",user.UserId.ToString()),
                                                new Claim(ClaimTypes.Role,user.RoleId.ToString())
                                            }),
                                            Expires = DateTime.UtcNow.AddHours(1),
                                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                                            SecurityAlgorithms.HmacSha256Signature)
                                        };

                                        var token = tokenHandler.CreateToken(tokenDescrptor);

                                        var wrtetoekn = tokenHandler.WriteToken(token);

                                        authResponse = new AuthResponse()
                                        {
                                            IsActive = true,
                                            JwtToken = wrtetoekn,
                                            StatusCode = 2000,
                                            StatusMessage = "valid user",
                                            ValidPassword = true,
                                            ValidUser = true
                                        };


                                    }
                                    else
                                    {
                                        authResponse = new AuthResponse()
                                        {
                                            IsActive = true,
                                            JwtToken = "",
                                            StatusCode = (int)AuthenticationStatusCodes.userinvalid,
                                            StatusMessage = "invalid password",
                                            ValidPassword = false,
                                            ValidUser = true
                                        };
                                    }
                                }
                                else
                                {
                                    authResponse = new AuthResponse()
                                    {
                                        IsActive = true,
                                        JwtToken = "",
                                        StatusCode = (int)AuthenticationStatusCodes.passwordnull,
                                        StatusMessage = "password required",
                                        ValidPassword = false,
                                        ValidUser = true
                                    };
                                }
                            }
                        }
                        else
                        {
                            authResponse = new AuthResponse()
                            {
                                IsActive = true,
                                JwtToken = "",
                                StatusCode = (int)AuthenticationStatusCodes.userinactive,
                                StatusMessage = "user inactive contact admin",
                                ValidPassword = false,
                                ValidUser = true
                            };
                        }


                    }
                    else
                    {
                        authResponse = new AuthResponse()
                        {
                            IsActive = false,
                            JwtToken = "",
                            StatusCode = (int)AuthenticationStatusCodes.userinvalid,
                            StatusMessage = "invalid user",
                            ValidPassword = false,
                            ValidUser = false
                        };
                    }
                }
                else
                {
                    authResponse = new AuthResponse()
                    {
                        IsActive = false,
                        JwtToken = "",
                        StatusCode = (int)AuthenticationStatusCodes.usernamenull,
                        StatusMessage = "username required",
                        ValidPassword = false,
                        ValidUser = false
                    };
                }
            }
            else
            {
                authResponse = new AuthResponse()
                {
                    IsActive = false,
                    JwtToken = "",
                    StatusCode = (int)AuthenticationStatusCodes.usernull,
                    StatusMessage = "invalid user",
                    ValidPassword = false,
                    ValidUser = false
                };
            }

            return authResponse;
        }

        public async Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse auth)
        {
            try
            {
                ApplicationUser applicationUser = null;

                var toeknKey = Encoding.ASCII.GetBytes(_tokenKey);
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(auth.JwtToken,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(toeknKey),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    }, out securityToken);
                var jwtTken = securityToken as JwtSecurityToken;

                if (jwtTken != null && jwtTken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                {
                    string username = principle.Identity.Name;

                    var user = await _dbContext.users.Where(x => x.Email.ToLower().Trim() == username.ToLower().Trim() || x.Phone.Trim() == username.Trim()).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        applicationUser = new ApplicationUser()
                        {
                            Id = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            CustomerId = user.CustomerId,
                            Email = user.Email,
                            FullName = user.FirstName + "" + user.LastName,
                            Phone = user.Phone,
                            RoleId = user.RoleId,
                            ProviderId = user.ProviderId
                        };
                    }

                }
                return applicationUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
