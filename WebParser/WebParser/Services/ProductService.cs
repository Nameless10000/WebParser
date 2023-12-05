using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebParser.Services
{
    public class ProductService
    {
        private readonly ParserContext _context;
        private readonly IMapper _mapper;

        public ProductService(ParserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Product> AddProductAsync(ProductAddDto productAddDto)
        {
            var product = _mapper.Map<Product>(productAddDto);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var createdProd = await _context.Products
                .Include(p => p.Name)
                .Include(p => p.Shop)
                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            return createdProd;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Shop)
                .Include(p => p.Name)
                .ToListAsync();
        }

        public async Task<Product?> GetProductAsync(uint productId)
        {
            return await _context.Products
                .Include(p => p.Shop)
                .Include(p => p.Name)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<List<BestPriceProduct>> GetBestOffersAsync()
        {
            var names = await _context.ProductsNames.ToListAsync();
            var bestOffers = new List<BestPriceProduct>();

            foreach (var name in names)
            {
                var bestOffer = await _context.Products
                    .Include(p => p.Name)
                    .Where(p => p.NameId == name.ProductsNamesId)
                    .OrderBy(p => p.LastFetchedPrice)
                    .FirstOrDefaultAsync();

                var offer = _mapper.Map<BestPriceProduct>(bestOffer);
                bestOffers.Add(offer);
            }

            return bestOffers;
        }
    }
}
