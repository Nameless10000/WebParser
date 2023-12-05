using Microsoft.EntityFrameworkCore;
using ParserDbContext;
using ParserDbContext.Entities;
using System.Text.RegularExpressions;

namespace WebParser.Services
{
    public partial class FixsenParserService
    {
        private readonly Regex _fixsenRegex = FixsenRegex();
        private readonly Regex _priceRegex = PriceRegex();
        private readonly HttpClient _http = new();
        private readonly ParserContext _context;

        public FixsenParserService(ParserContext context)
        {
            _context = context;
        }

        public async Task ParseUrls()
        {
            var products = await _context.Products
                .Where(p => p.ShopId == 1)
                .ToListAsync();

            foreach (var product in products)
            {
                var data = await _http.GetStringAsync(product.Url);
                var priceString = _fixsenRegex.Match(data).Value;
                var price = double.Parse(_priceRegex.Match(priceString).Value);
                product.LastFetchedPrice = price;

                _context.Products.Update(product);
            }

            await _context.SaveChangesAsync();
        }

        [GeneratedRegex(@"<p class=""price[\sa-zA-Z_-]*"">[a-zA-Z<>]*<span class=""woocommerce-Price-amount amount[\sa-zA-Z_-]*""><bdi>\d+")]
        private static partial Regex FixsenRegex();
        [GeneratedRegex(@"\d+")]
        private static partial Regex PriceRegex();
    }
}
