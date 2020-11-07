using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shb.Api.Wrappers;
using Shb.Domain.Abstractions;
using System.Linq;
using System.Text.Json;

namespace Shb.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAppLogger<ErrorHandlerMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { 
                    Succeeded = false, 
                    Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                };
                
                switch (ex)
                {
                    case System.ComponentModel.DataAnnotations.ValidationException e:
                        logger.LogError(e, e.Message);
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    case FluentValidation.ValidationException e:
                        logger.LogError(e, e.Message);
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        foreach(var er in e.Errors) {
                            responseModel.Errors.Append(er.ErrorMessage);
                        }
                        break;
                    case KeyNotFoundException e:
                        logger.LogError(e, e.Message);
                        response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                    default:
                        // unhandled error
                        logger.LogError(ex, ex.Message);
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);                

                await response.WriteAsync(result);
            }
        }        
    }
}
