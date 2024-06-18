using Domain;

namespace Infrastructure.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetClientByIdAsync(int id);
    }
}
