using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Events.Services;

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
                    BlobStorage _blobStorage = new BlobStorage("profile-uploads", Environment.GetEnvironmentVariable("UploadStorage"));

                    var file = formData.Files[0];
                    var filePath = Path.GetTempFileName();
                    Guid fileNamePrefixGuid = Guid.NewGuid();

                    using (var stream = new MemoryStream())
                    {
                        file.CopyToAsync(stream).ConfigureAwait(false);
                        _blobStorage.UploadFile(fileNamePrefixGuid + file.FileName, stream);
                    }
                }
                return new JsonResult(new { message = "Photo upload succeeded!" });
            }
            catch (Exception)
            {
                return new JsonResult(new { message = "Photo failed to upload." });
            }


        }
    }
}
