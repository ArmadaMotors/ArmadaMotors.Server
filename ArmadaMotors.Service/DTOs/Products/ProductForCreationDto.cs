using ArmadaMotors.Domain.Enums;
using ArmadaMotors.Service.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ArmadaMotors.Service.DTOs.Products
{
    public class ProductForCreationDto
    {
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public decimal Price { get; set; }
        public CurrencyType CurrencyType { get; set; }

        [Required(ErrorMessage = "Please, select file ...")]
        [AllowedExtensions(new string[] { ".jpg", ".png" }, isArray: true)]
        [MaxFileSize(5 * 1024 * 1024, isArray: true)]
        [DataType(DataType.Upload)]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}