using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure
{
    public sealed class DapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var dataSource = _configuration["dataSource"];
            var initialCatalog = _configuration["Initial Catalog"];

            string connectionString = $"Server={dataSource};Initial Catalog={initialCatalog};Trusted_Connection=True;TrustServerCertificate=True";

            return new SqlConnection(connectionString);
        }
    }
}
