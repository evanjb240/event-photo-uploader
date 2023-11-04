
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Events.Databases;
using Events.Interfaces;

namespace Events.Services
{
    public class RSVPStorageService : IRSVPStorageService
    {
        private const string TableName = "RSVP";
        private string _connectionString;

        public RSVPStorageService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<RSVPEntity> GetEntityAsync(string code, string lastName)
        {
            var tableClient = await GetTableClient();

            var ret = tableClient.QueryAsync<RSVPEntity>(x=> x.Code == code && x.LastName == lastName, 1);
            if(ret == null){
                return null;
            }
            return await ret.FirstOrDefaultAsync();;  
        }
         public async Task<List<RSVPEntity>> GetAllEntitiesAsync()
        {
            var tableClient = await GetTableClient();

            var ret = tableClient.QueryAsync<RSVPEntity>();
            if(ret == null){
                return null;
            }
            
            var rsvpList = await ret.ToListAsync();

            rsvpList = rsvpList.OrderBy(x=> x.RSVPDecision).ToList();

            return rsvpList;  
        }
        public async Task<List<RSVPEntity>> GetEntityByCodeAsync(string code)
        {
            var tableClient = await GetTableClient();

            var ret = tableClient.QueryAsync<RSVPEntity>(x=> x.Code == code);
            if(ret == null){
                return null;
            }
            return await ret.ToListAsync();;  
        }

        public async Task<RSVPEntity> UpsertEntityAsync(RSVPEntity entity)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(entity);
            return entity;
        }

        public async Task DeleteEntityAsync(string code, string id)
        {
            var tableClient = await GetTableClient();
            await tableClient.DeleteEntityAsync(code, id);
        }

        private async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(_connectionString);

            var tableClient = serviceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }
    }
}