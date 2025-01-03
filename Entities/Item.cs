using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace aHoang.Entities
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string ImageFileName { get; set; }
        public string? Category { get; set; }
        public string? Class { get; set; }
        public string? Author { get; set; }
        public long? Price { get; set; }
        public string? ItemCode { get; set; }
    }
}
