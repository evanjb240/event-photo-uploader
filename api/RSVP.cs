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

namespace Events.RSVp
{
    public static class RSVP
    {
        [FunctionName("RSVP")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                RSVPStorageService _storageService = new RSVPStorageService(Environment.GetEnvironmentVariable("UploadStorage"));
                RSVPEntity entity = new RSVPEntity(){
                    Code = "12345",
                    FirstName = "Evan",
                    LastName = "Bauer",
                    PlusOne = 1
                };

                entity.PartitionKey = entity.Code;
                string Id = Guid.NewGuid().ToString();
                entity.Id = Id;
                entity.RowKey = Id;
                var createdEntity = await _storageService.UpsertEntityAsync(entity);
                return new JsonResult(new { message = "RSVP successful!" });
            }
            catch (Exception e)
            {
                return new JsonResult(new { message = e.Data + "RSVP failed to save." });
            }
        }
    }
}
