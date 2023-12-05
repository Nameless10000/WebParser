using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebParser.Models;

namespace WebParser.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : DefaultController
    {
        private readonly ProductService _productService;
        private readonly ProductsNamesService _namesService;

        public ProductsController(ProductService productService, ProductsNamesService namesService)
        {
            _productService = productService;
            _namesService = namesService;
        }

        [HttpPost]
        public async Task<JsonResult> AddProduct(ProductAddDto productAddDto)
        {
            return Json(await _productService.AddProductAsync(productAddDto));
        }

        [HttpGet]
        public async Task<JsonResult> GetProducts()
        {
            return Json(await _productService.GetProductsAsync());
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<JsonResult> GetProduct(uint productId)
        {
            return Json(await _productService.GetProductAsync(productId));
        }

        [HttpGet]
        public async Task<JsonResult> GetNames()
        {
            return Json(await _namesService.GetNamesAsync());
        }

        [HttpGet]
        public async Task<JsonResult> AddName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Json(new
                {
                    Error = "empty <name> param"
                });

            return Json(await _namesService.AddProductNameAsync(name));
        }

        [HttpGet]
        public async Task<JsonResult> GetBestOffers()
        {
            return Json(await _productService.GetBestOffersAsync());
        }
    }
}
