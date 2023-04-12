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
    public static class RSVPSubmit
    {
        [FunctionName("RSVPSubmit")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = String.Empty;
                using (StreamReader streamReader =  new  StreamReader(req.Body))
                {
                    requestBody = await streamReader.ReadToEndAsync();
                }
                List<RSVPEntity> entities = JsonConvert.DeserializeObject<List<RSVPEntity>>(requestBody);  
                RSVPStorageService _storageService = new RSVPStorageService(Environment.GetEnvironmentVariable("UploadStorage"));

                List<RSVPEntity> upsertedEntities = new List<RSVPEntity>();
                foreach(RSVPEntity entity in entities){
                    if(string.IsNullOrEmpty(entity.PartitionKey)){
                        entity.PartitionKey = entity.Code;
                    }
                    if(string.IsNullOrEmpty(entity.RowKey)){
                        string id = Guid.NewGuid().ToString();
                        entity.Id = id;
                        entity.RowKey = entity.FirstName + entity.LastName;
                    }

                    //var returnedEntity = await _storageService.GetEntityAsync(entity.Code, entity.LastName);
                    /*if (returnedEntity == null)
                    {
                        return new JsonResult(new { StatusCodes.Status404NotFound, message = "Code combination not found" });
                    }*/
                    var createdEntity = await _storageService.UpsertEntityAsync(entity);
                    if(createdEntity != null){
                        upsertedEntities.Add(createdEntity);
                    }
                }

                return new JsonResult(new { StatusCodes.Status200OK, message = upsertedEntities });

            }
            catch (Exception e)
            {
                return new JsonResult(new { message = e.Message + "RSVP failed to save." + req.Body });
            }
        }
    }
}
