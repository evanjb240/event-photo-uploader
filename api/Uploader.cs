using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Events.Repositories;

namespace Events.Uploader
{
    public static class Uploader
    {
        [FunctionName("Uploader")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var formData = await req.ReadFormAsync();

                if (formData?.Files?.Count > 0)
                {
                    AzureStorage _blobStorage = new AzureStorage(Environment.GetEnvironmentVariable("UploadStorage"), "profile-uploads");

                    var file = formData.Files[0];
                    var upload = await _blobStorage.UploadAsync(file);
                    return new JsonResult(new { message = "Photo upload succeeded!" });
                }
            }
            catch (Exception)
            {
                return new JsonResult(new { message = "Photo failed to upload." });
            }
            return new JsonResult(new { message = "Photo failed to upload." });
        }
    }
}
