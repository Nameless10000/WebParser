using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace WebParser.Services
{
    public partial class GroheParserService
    {
        private readonly Regex _priceRegex = PriceRegex();
        private readonly Regex _priceExtractRegex = PriceExtractRegex();
        private readonly Regex _priceFilterRegex = PriceFilterRegex();
        private readonly ParserContext _context;
        private readonly HttpClient _httpClient = new();

        public GroheParserService(ParserContext context)
        {
            _context = context;
        }

        public async Task ParseUrls()
        {
            var products = await _context.Products
                .Where(p => p.ShopId == 2)
                .ToListAsync();

            foreach (var product in products)
            {
                var data = await _httpClient.GetStringAsync(product.Url);
                var priceString = _priceRegex.Match(data).Value;
                var filteredString = _priceFilterRegex.Replace(priceString, "");
                var price = new Regex(@"(&nbsp;|\s)*").Replace(_priceExtractRegex.Match(filteredString).Value, "");

                product.LastFetchedPrice = double.Parse(price);
                _context.Products.Update(product);
            }

            await _context.SaveChangesAsync();
        }

        [GeneratedRegex("<span class=\"price productproduct-price__price\"[\\s\\d[a-zA-Z-_\"=]*>\\d+(&nbsp;|\\s)*\\d*(&nbsp;|\\s)*\\d*")]
        private static partial Regex PriceRegex();
        [GeneratedRegex("\\d{1,3}(\\s+\\d{0,3})*")]
        private static partial Regex PriceExtractRegex();
        [GeneratedRegex("\\d{4,}")]
        private static partial Regex PriceFilterRegex();
    }
}
