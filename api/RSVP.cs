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
using Events.Databases;
using Events.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Events.RSVp
{
    public static class RSVP
    {
        [FunctionName("RSVPQuery")]
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
                RSVPEntity entity = JsonConvert.DeserializeObject<RSVPEntity>(requestBody);  
                RSVPStorageService _storageService = new RSVPStorageService(Environment.GetEnvironmentVariable("UploadStorage"));
                
                if(entity != null){
                    entity.Code = entity.Code.ToUpper();
                }

                var foundEntity = await _storageService.GetEntityAsync(entity.Code, entity.LastName);
                if (foundEntity == null)
                {
                    return new JsonResult(new { StatusCodes.Status404NotFound, message = "Code combination not found" });
                }
                List<RSVPEntity> foundEntities = await _storageService.GetEntityByCodeAsync(entity.Code);

                return new JsonResult(new { StatusCodes.Status200OK, foundEntities = foundEntities });

            }
            catch (Exception e)
            {
                return new JsonResult(new { message = e.Message + "RSVP failed to save." + req.Body });
            }
        }
    }
}
