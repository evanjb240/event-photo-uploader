
using System.Threading.Tasks;
using Azure.Data.Tables;
using Events.Databases;
using Events.Interfaces;
using Microsoft.Extensions.Configuration;

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

        public async Task<RSVPEntity> GetEntityAsync(string code, string id)
        {
            var tableClient = await GetTableClient();
            return await tableClient.GetEntityAsync<RSVPEntity>(code, id);
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