using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Events.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Events.Repositories;
using Events.Models.Blob;

namespace Events.Uploader
{
    public static class BlobLister
    {
        [FunctionName("BlobLister")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                AzureStorage storage = new AzureStorage(Environment.GetEnvironmentVariable("UploadStorage"), "profile-uploads");
                #nullable enable
                List<BlobDTO>? files = await storage.ListAsync();
                return new JsonResult(new {StatusCodes.Status200OK, files});
            }
            catch (Exception)
            {
                return new JsonResult(new { message = "Get all files failed." });
            }


        }
    }
}
