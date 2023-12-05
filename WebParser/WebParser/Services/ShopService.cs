using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebParser.Services
{
    public class ShopService
    {
        private readonly ParserContext _context;
        private readonly IMapper _mapper;

        public ShopService(ParserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Shop> AddShopAsync(ShopAddDto shopAddDto)
        {
            var shop = _mapper.Map<Shop>(shopAddDto);

            await _context.Shops.AddAsync(shop);
            await _context.SaveChangesAsync();

            return shop;
        }

        public async Task<List<Shop>> GetShopsAsync()
        {
            return await _context.Shops.ToListAsync();
        }
    }
}
