namespace ParserDbContext.Entities
{
    public class Shop
    {
        [Key]
        public uint ShopId { get; set; }
        public string Name { get; set; }
    }
}
