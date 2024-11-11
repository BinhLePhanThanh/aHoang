namespace aHoang.DTO
{
    public class ItemDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public IFormFile File { get; set; }
    }
}
