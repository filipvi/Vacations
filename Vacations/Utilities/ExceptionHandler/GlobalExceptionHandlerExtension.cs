using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vacations.Utilities.Extensions;
using Vacations.Utilities.Logger;

namespace Vacations.Utilities.ExceptionHandler
{
    public static class GlobalExceptionHandlerExtension
    {
        //This method will globally handle logging unhandled execeptions.
        //It will respond json response for ajax calls that send the json accept header
        //otherwise it will redirect to an error page
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app, string errorPagePath,
            bool respondWithJsonErrorDetails = false)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    //============================================================
                    //Log Exception
                    //============================================================
                    var exception = context.Features.Get<IExceptionHandlerFeature>().Error;

                    string errorDetails = $"{Environment.NewLine}" +
                                          $"Time: {DateTime.Now.DateToString() + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second} {Environment.NewLine}" +
                                          $"Message: {exception.Message} {Environment.NewLine}";

                    int statusCode = (int)HttpStatusCode.InternalServerError;

                    context.Response.StatusCode = statusCode;

                    var problemDetails = new ProblemDetails
                    {
                        Title = "Unexpected Error",
                        Status = statusCode,
                        Detail = errorDetails,
                        Instance = Guid.NewGuid().ToString()
                    };

                    var sb = new StringBuilder();
                    sb.AppendLine();
                    sb.Append("ERROR: ");
                    sb.Append(" " + problemDetails.Title);
                    sb.AppendLine();
                    sb.Append("VacationRequestStatus " + problemDetails.Status);
                    sb.AppendLine();
                    sb.Append("Detalji: " + problemDetails.Detail);

                    Log.LogGlobalError(sb.ToString());

                    var json = JsonConvert.SerializeObject(errorDetails);

                    //============================================================
                    //Return response
                    //============================================================
                    var matchText = "JSON";

                    bool requiresJsonResponse = context.Request
                        .GetTypedHeaders()
                        .Accept
                        .Any(t => t.Suffix.Value?.ToUpper() == matchText
                                  || t.SubTypeWithoutSuffix.Value?.ToUpper() == matchText);

                    if (requiresJsonResponse)
                    {
                        context.Response.ContentType = "application/json; charset=utf-8";

                        if (!respondWithJsonErrorDetails)
                            json = JsonConvert.SerializeObject(new
                            {
                                Title = "Unexpected Error",
                                Status = statusCode
                            });
                        await context.Response
                            .WriteAsync(json, Encoding.UTF8);
                    }
                    else
                    {
                        context.Response.Redirect(errorPagePath);

                        await Task.CompletedTask;
                    }
                });
            });
        }
    }
}
