using DataStreamingWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;

namespace DataStreamingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ValuesController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task GetProducts()
        {
            Response.ContentType = "application/json"; // Set content type here

            await Response.WriteAsync("["); // Start the JSON array

            bool firstItem = true;

            await foreach (var product in _productRepository.GetProductsAsync())
            {
                if (!firstItem)
                {
                    await Response.WriteAsync(","); // Add a comma for subsequent items
                }
                else
                {
                    firstItem = false; // First item, no comma needed
                }
                await Response.WriteAsync(JsonSerializer.Serialize(product));
                //await Response.WriteAsJsonAsync(product); // Write each product as JSON
                await Response.Body.FlushAsync(); // Flush the response body
            }

            await Response.WriteAsync("]"); // Close the JSON array
        }
    }
}
