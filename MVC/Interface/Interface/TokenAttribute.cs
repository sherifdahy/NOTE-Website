using Entities.Models;
using Interface.Dto;
using Interface.Services.ApiCall;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Interface
{
    public class TokenAttribute : ActionFilterAttribute
    {
        private readonly IApiCall _apiCall;
        private readonly UserManager<applicationUser> _userService;

        // Constructor injection for dependencies
        public TokenAttribute(IApiCall apiCall, UserManager<applicationUser> userService)
        {
            _apiCall = apiCall;
            _userService = userService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string? token = context.HttpContext.Session.GetString("access_token");
            string? expire = context.HttpContext.Session.GetString("expire_date");

            if (token == null)
            {
                var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null)
                {
                    var applicationUser = await _userService.FindByIdAsync(userId);
                    if (applicationUser != null)
                    {
                        ResponseDTO<AuthenticatePosDTO, string> responseDTO = await _apiCall.GenerateNewTokenAsync<AuthenticatePosDTO, string>("connect/token", applicationUser);
                        var newexpireDatetime = DateTime.UtcNow.AddSeconds(int.Parse(responseDTO.SuccessResponse.expires_in));
                        context.HttpContext.Session.SetString("access_token", responseDTO.SuccessResponse.access_token);
                        context.HttpContext.Session.SetString("expire_date", newexpireDatetime.ToString("o"));
                    }
                }
            }
            else
            {
                DateTime expire_datetime = DateTime.Parse(expire);
                var diff = expire_datetime - DateTime.UtcNow;
                if (diff.TotalMinutes < 5)
                {
                    var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (userId != null)
                    {
                        var applicationUser = await _userService.FindByIdAsync(userId);

                        if (applicationUser != null)
                        {
                            ResponseDTO<AuthenticatePosDTO, string> responseDTO = await _apiCall.GenerateNewTokenAsync<AuthenticatePosDTO, string>("connect/token", applicationUser);
                            var newexpireDatetime = DateTime.UtcNow.AddSeconds(int.Parse(responseDTO.SuccessResponse.expires_in));
                            context.HttpContext.Session.SetString("access_token", responseDTO.SuccessResponse.access_token);
                            context.HttpContext.Session.SetString("expire_date", newexpireDatetime.ToString("o"));
                        }
                    }
                }
            }

            await next();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}