namespace aHoang.DTO
{
    public class ItemDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        
        public string? Category { get; set; }
        public string? Class { get; set; }
        public string? Author { get; set; }
        public long? Price { get; set; }
        public string? ItemCode { get; set; }
        public IFormFile File { get; set; }
    }
}
