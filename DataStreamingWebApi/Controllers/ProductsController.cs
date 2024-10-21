using DataStreamingWebApi.DTOs;
using DataStreamingWebApi.Models;
using DataStreamingWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataStreamingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductsController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<Record>>> GetProducts(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productRepository.GetPaginatedProducts(pageNumber, pageSize);
            var totalCount = await _productRepository.GetTotalProductsCount();

            var response = new PaginatedResponse<Record>
            {
                Items = products,
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetData([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            // Fetch all data from the repository
            var allData = await _productRepository.GetAllData();

            // In-memory pagination using Skip and Take
            var paginatedData = allData.Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToList();

            return Ok(paginatedData);
        }
               

    }
}
