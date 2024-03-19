using Microsoft.AspNetCore.Http;
using Finance.Core.Global;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Application.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var msg = error is AppException ? ((AppException)error).ErrorMessage : error.Message;

                switch (error)
                {
                    case AppException e:
                        {
                            context.Response.Clear();
                            context.Response.ContentType = "text/plain";
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsync("*_*" + ((AppException)error).ErrorMessage + "*_*");
                            return;
                        }
                    default:
                        {
                            context.Response.Clear();
                            context.Response.ContentType = "text/plain";
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await context.Response.WriteAsync("*_*" + "Internal Server Error" + "*_*");
                            return;

                        }
                }
            }
        }

        public class BadRequestResult
        {
            public string ErrorMsg { get; set; }
            public int ErrorNo { get; set; }

        }
    }
}
