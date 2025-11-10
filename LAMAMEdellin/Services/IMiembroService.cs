using LAMAMEdellin.client.Models;
using System.Net.Http.Json;

namespace LAMAMedellin.Client.Services
{
    public interface IMiembroService
    {
        Task<List<Miembro>> GetMiembrosAsync();
        Task<Miembro?> GetMiembroAsync(int id);
        Task<bool> CreateMiembroAsync(Miembro miembro);
        Task<bool> UpdateMiembroAsync(int id, Miembro miembro);
        Task<bool> DeleteMiembroAsync(int id);
    }
}