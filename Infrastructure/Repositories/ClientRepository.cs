using Dapper;
using Domain;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public sealed class ClientRepository : IClientRepository
    {
        private readonly DapperContext _dapperContext;

        public ClientRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var query = @"SELECT ID, NAME, EMAIL FROM CLIENT WHERE ID = @ID";

            Client client;

            using (var connection = _dapperContext.CreateConnection())
            {
                client = await connection.QueryFirstOrDefaultAsync<Client>(query, new { Id = id});
            }

            return client;
        }
    }
}
