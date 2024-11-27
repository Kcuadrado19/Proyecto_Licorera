using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Proyecto_Licorera_Corchos.web.Services;

namespace Proyecto_Licorera_Corchos.web.Attributes

{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string permission, string module) : base(typeof(CustomAuthorizeFilter))
        {
            Arguments = new object[] { permission, module };
        }
    }

    public class CustomAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        private readonly string _module;
        private readonly IUserService _userService;

        public CustomAuthorizeFilter(string permission, string module, IUserService userService)
        {
            _permission = permission;
            _module = module;
            _userService = userService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool isAuthorized = await _userService.CurrentUserIsAuthorizedAsync(_permission, _module);

            if (!isAuthorized)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
