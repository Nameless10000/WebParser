namespace ParserDbContext.Entities
{
    public class Product
    {
        [Key]
        public uint ProductId { get; set; }
        public string Url { get; set; }
        public double? LastFetchedPrice { get; set; }
        public uint NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public ProductsNames Name { get; set; }
        public uint ShopId { get; set; }
        [ForeignKey(nameof(ShopId))]
        public Shop Shop { get; set; }
    }
}
