namespace WebParser.Models
{
    public class ProductAddDto
    {
        [Required]
        public string Url { get; set; }
        [Required]
        public uint ShopId { get; set; }
        [Required]
        public uint NameId { get; set; }
    }
}
