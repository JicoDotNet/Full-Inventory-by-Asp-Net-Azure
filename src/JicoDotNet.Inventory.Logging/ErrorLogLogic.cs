using JicoDotNet.Inventory.Core.Entities;
using Newtonsoft.Json;
using System;
using System.IO;

namespace JicoDotNet.Inventory.Logging
{
    public class ErrorLogLogic
    {
        public static void Logging(IErrorLog errorLog)
        {
            if (errorLog != null)
            {
                string message = string.Empty;
                message += Environment.NewLine;
                message += "-------------------------------------------------\n";
                message += $"Time: {GenericLogic.IstNow:dd/MMM/yyyy hh:mm:ss tt}";
                message += Environment.NewLine;
                message += $"RequestId: {errorLog.RequestId}";
                message += Environment.NewLine;
                message += "-------------------------------------------------";
                message += Environment.NewLine;
                message += $"Message: {errorLog.Exception?.Message}";
                message += Environment.NewLine;
                message += $"StackTrace: {errorLog.Exception?.StackTrace}";
                message += Environment.NewLine;
                message += $"Source: {errorLog.Exception?.Source}";
                message += Environment.NewLine;
                message += $"TargetSite: {errorLog.Exception?.TargetSite}";
                message += Environment.NewLine;
                message += $"HResult: {errorLog.Exception?.HResult}";
                message += Environment.NewLine;
                message += $"HttpMethod: {errorLog.HttpMethod}";
                message += Environment.NewLine;
                message +=
                    $"Path: {errorLog.HttpMethod + " :-> /" + errorLog.ControllerName + "/" + errorLog.ActionName + "/" + errorLog.UrlParameterId + "/" + errorLog.UrlParameterId2}";
                message += Environment.NewLine;
                message += $"Data: {JsonConvert.SerializeObject(errorLog.Exception?.Data)}";
                message += Environment.NewLine;
                message += $"InnerException: {JsonConvert.SerializeObject(errorLog.Exception?.InnerException)}";
                message += Environment.NewLine;
                message += "___________________________________________________________\n";
                message += "===========================================================\n";
                message += Environment.NewLine;

                if (!Directory.Exists(errorLog.FolderPath))
                {
                    Directory.CreateDirectory(errorLog.FolderPath);
                }
                errorLog.FolderPath += "/Error.log";
                using (StreamWriter writer = new StreamWriter(errorLog.FolderPath, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
        }
    }
}
