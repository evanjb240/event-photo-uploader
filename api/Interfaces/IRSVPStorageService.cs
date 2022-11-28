using System.Threading.Tasks;
using Events.Databases;

namespace Events.Interfaces{
    public interface IRSVPStorageService
    {
        Task<RSVPEntity> GetEntityAsync(string code, string id);
        Task<RSVPEntity> UpsertEntityAsync(RSVPEntity entity);
        Task DeleteEntityAsync(string code, string id);
    }
}
