using System.ComponentModel.DataAnnotations;

namespace Feirinha.Domains
{
    public class Products
    {
        [Key]
        public Guid IdProduct { get; set; }
        public string? Name { get; set; }

        public decimal Price{ get; set; }
    }
}
