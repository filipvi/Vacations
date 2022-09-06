using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Vacations.Utilities.Logger
{
    public static class Log
    {
        private static readonly ILogger Logger = ApplicationLogging.CreateLogger("AppLogger");

        public static void ControllerLog(Controller controller, Exception e, int? id)
        {
            try
            {
                string error;
                if (id == null)
                {
                    error = $"{Environment.NewLine}" +
                            $"Date and time: {DateTime.Now} {Environment.NewLine}" +
                            $"Message: {e.Message} {Environment.NewLine}" +
                            $"Controller: {controller.ControllerContext.RouteData.Values["controller"]} {Environment.NewLine}" +
                            $"Action: {controller.ControllerContext.RouteData.Values["action"]} {Environment.NewLine}" +
                            $"Full exception: {e} " +
                            $"{Environment.NewLine}";
                }
                else
                {
                    error = $"{Environment.NewLine}" +
                            $"Date and time: {DateTime.Now} {Environment.NewLine}" +
                            $"Message: {e.Message} {Environment.NewLine}" +
                            $"Controller: {controller.ControllerContext.RouteData.Values["controller"]} {Environment.NewLine}" +
                            $"Action: {controller.ControllerContext.RouteData.Values["action"]} {Environment.NewLine}" +
                            $"Id: {id} {Environment.NewLine}" +
                            $"Full exception: {e}" +
                            $"{Environment.NewLine}";
                }

                Logger.LogError(error);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static void RepositoryLog(string repository, string method, Exception e, int? id)
        {
            try
            {
                string error;
                if (id == null)
                {
                    error = $"{Environment.NewLine}" +
                            $"Date and time: {DateTime.Now} {Environment.NewLine}" +
                            $"Message: {e.Message} {Environment.NewLine}" +
                            $"Repository: {repository} {Environment.NewLine}" +
                            $"Method: {method} {Environment.NewLine}" +
                            $"Full exception: {e} " +
                            $"{Environment.NewLine}";
                }
                else
                {
                    error = $"{Environment.NewLine}" +
                            $"Date and time: {DateTime.Now} {Environment.NewLine}" +
                            $"Message: {e.Message} {Environment.NewLine}" +
                            $"Repository: {repository} {Environment.NewLine}" +
                            $"Method: {method} {Environment.NewLine}" +
                            $"Id: {id} {Environment.NewLine}" +
                            $"Full exception: {e} " +
                            $"{Environment.NewLine}";
                }
                Logger.LogError(error);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static void LogGlobalError(string json)
        {
            Logger.LogError(json);
        }

        public static void InfoLog(string message)
        {
            var info = $"{Environment.NewLine}" +
                       $"Date and time: {DateTime.Now} {Environment.NewLine}" +
                       $"Message: {message} {Environment.NewLine}";

            Logger.LogInformation(info);
        }

        public static void ErrorLog(string message)
        {
            Logger.LogError(message);
        }
    }
}
