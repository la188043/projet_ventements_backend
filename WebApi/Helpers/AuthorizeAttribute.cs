using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Application;
using Application.Services.Users.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public AuthorizeAttribute()
        {
        }

        public AuthorizeAttribute(string roles)
        {
            Roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (OutputDtoQueryUser) context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new {message = "Not authorized access"})
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };

                return;
            }

            if (Roles == "Admin")
            {
                var token = context.HttpContext.Request.Headers["Authorization"]
                    .FirstOrDefault()?.Split(" ").Last();
                
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken) validatedToken;
                    string userRole = jwtToken.Claims.FirstOrDefault(x => x.Type == "role")?.Value;
                    
                    if (userRole  != "Admin")
                    {
                        context.Result = new JsonResult(new {message = "Not authorized access"})
                        {
                            StatusCode = StatusCodes.Status401Unauthorized
                        };
                    }
                }
                catch
                {}
            }

        }
    }
}