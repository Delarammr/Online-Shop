using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PajoPhone.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Color { get; set; }
        public required string Description { get; set; }
        public decimal Price { get ; set; }
        [NotMapped]
        public IFormFile Image { get; set; } = null!;
        public string? Path { get; set; }
    }
}
