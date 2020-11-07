using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shb.Api.Wrappers;

namespace Shb.Api.Attributes
{
    public class ValidationFilterAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                var errors = modelState.Values.SelectMany(v => v.Errors).Select(m => m.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(new Response<string>() {
                    Succeeded = false,
                    Errors = errors                    
                });
            }
            
            await next();
        }
    }
}
