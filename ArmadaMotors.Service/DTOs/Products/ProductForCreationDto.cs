using Microsoft.AspNetCore.Http;

namespace ArmadaMotors.Service.DTOs.Products
{
    public class ProductForCreationDto
    {
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}