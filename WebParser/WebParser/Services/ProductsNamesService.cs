using Microsoft.EntityFrameworkCore;

namespace WebParser.Services
{
    public class ProductsNamesService
    {
        private readonly ParserContext _context;

        public ProductsNamesService(ParserContext context)
        {
            _context = context;
        }


        public async Task<ProductsNames?> AddProductNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            var productName = new ProductsNames
            {
                Name = name
            };

            await _context.ProductsNames.AddAsync(productName);
            await _context.SaveChangesAsync();
            return productName;
        }

        public async Task<List<ProductsNames>> GetNamesAsync()
        {
            return await _context.ProductsNames.ToListAsync();
        }
    }
}
