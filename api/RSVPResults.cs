using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Events.Services;

namespace Events.RSVp
{
    public static class RSVPResults
    {
        [FunctionName("RSVPResults")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = String.Empty;
                using (StreamReader streamReader =  new  StreamReader(req.Body))
                {
                    requestBody = await streamReader.ReadToEndAsync();
                }
                string entity = requestBody;  
                RSVPStorageService _storageService = new RSVPStorageService(Environment.GetEnvironmentVariable("UploadStorage"));
                 
                if(entity != null){

                }

                var foundEntities = await _storageService.GetAllEntitiesAsync();
                if (foundEntities == null)
                {
                    return new JsonResult(new { StatusCodes.Status404NotFound, message = "No found RSVPs" });
                }

                return new JsonResult(new { StatusCodes.Status200OK, foundEntities = foundEntities });

            }
            catch (Exception e)
            {
                return new JsonResult(new { message = e.Message + "RSVP failed to save." + req.Body });
            }
        }
    }
}
