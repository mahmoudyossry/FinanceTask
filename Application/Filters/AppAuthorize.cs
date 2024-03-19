using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Finance.Application.Dto;
using Finance.Application.Services.Interfaces;
using Finance.Core.Entities;
using Finance.Core.Global;
using Finance.Infrastructure.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Application.Filters
{
    //Extenting from AuthorizeAttribute or Attribute is upto user choice.
    //You can consider using AuthorizeAttribute if you want to use the predefined properties and functions from Authorize Attribute.
    public class AppAuthorize : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public string Permissions { get; set; } //Permission string to get from controller

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //The below line can be used if you are reading permissions from token
            //var permissionsFromToken=context.HttpContext.User.Claims.Where(x=>x.Type=="Permissions").Select(x=>x.Value).ToList()

            //Identity.Name will have windows logged in user id, in case of Windows Authentication
            //Indentity.Name will have user name passed from token, in case of JWT Authenntication and having claim type "ClaimTypes.Name"
            var userName = context.HttpContext.User.Identity.Name;
            var userId = context.HttpContext.User.Identities.FirstOrDefault().FindFirst("userId").Value;
                       
            var lang = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "lang").Value;

            var globalInfo = context.HttpContext.RequestServices.GetService<GlobalInfo>();
            globalInfo.SetValues(userName, userId,lang);

            var userManager = context.HttpContext.RequestServices.GetService<ApplicationUserManager>();


            var requiredPermissions = Permissions?.Split(','); //Multiple permissiosn can be received from controller, delimiter "," is used to get individual values

            bool hasAllowAnonymous = 
                context.ActionDescriptor.EndpointMetadata
                            .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));

            if (!hasAllowAnonymous && requiredPermissions?.Length > 0)
            {
                var grantedPermissions = await userManager.GetUserPermission(globalInfo.UserId);
                if(!grantedPermissions.Any(x=> requiredPermissions.Contains(x)))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }

            return;
        }

    }
}