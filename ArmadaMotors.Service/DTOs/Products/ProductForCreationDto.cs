using ArmadaMotors.Domain.Commons;
using ArmadaMotors.Domain.Enums;
using ArmadaMotors.Service.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ArmadaMotors.Service.DTOs.Products
{
    public class ProductForCreationDto : ILanguageEntityName, ILanguageEntityDescription
    {
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public string DescriptionUz { get; set; }
        public string DescriptionRu { get; set; }
        public string DescriptionEn { get; set; }

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