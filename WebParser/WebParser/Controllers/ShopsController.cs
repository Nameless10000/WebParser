using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WebParser.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopsController : DefaultController
    {
        private readonly ShopService _shopService;

        public ShopsController(ShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost]
        public async Task<JsonResult> AddShop([FromBody] ShopAddDto shopAddDto)
        {
            return Json(await _shopService.AddShopAsync(shopAddDto));
        }

        [HttpGet]
        public async Task<JsonResult> GetShops()
        {
            return Json(await _shopService.GetShopsAsync());
        }
    }
}
