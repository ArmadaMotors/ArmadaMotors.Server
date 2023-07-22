using ArmadaMotors.Domain.Commons;
using ArmadaMotors.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaMotors.Service.DTOs.Products
{
    public class ProductForUpdateDto : ILanguageEntityName, ILanguageEntityDescription
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
    }
}
