using Dapper;
using DataStreamingWebApi.Data;
using DataStreamingWebApi.Models;
using System.Data.Common;

namespace DataStreamingWebApi.Services
{
    public class ProductRepository
    {
        private readonly DapperContext _connectionString;

        public ProductRepository(DapperContext connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Record>> GetPaginatedProducts(int pageNumber, int pageSize)
        {
            using (var db = _connectionString.CreateConnection())
            {
                var sql = "SELECT * FROM Records ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";
                return await db.QueryAsync<Record>(sql, new { Offset = (pageNumber - 1) * pageSize, PageSize = pageSize });
            }
        }

        public async Task<int> GetTotalProductsCount()
        {
            using (var db = _connectionString.CreateConnection())
            {
                var sql = "SELECT COUNT(*) FROM Records;";
                return await db.ExecuteScalarAsync<int>(sql);
            }
        }


        // Fetch all data without pagination
        public async Task<IEnumerable<Product>> GetAllData()
        {
            using (var db = _connectionString.CreateConnection())
            {
                var query = "SELECT * FROM Products";
                return await db.QueryAsync<Product>(query);
            }
        }

        // Streaming data using Dapper
        public async IAsyncEnumerable<Product> GetProductsAsync()
        {
                using (var db = _connectionString.CreateConnection())
                {
                    var query = "SELECT * FROM Products where Id<10";

                // Use QueryAsync to fetch all products as a list
                var products = await db.QueryAsync<Product>(query);

                // Yield each product asynchronously
                foreach (var product in products)
                {
                    yield return product; // Yield each product
                }
            }
           
        }
    }
}
