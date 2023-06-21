using ArmadaMotors.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaMotors.Service.DTOs.Products
{
    public class ProductForUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public decimal Price { get; set; }
        public CurrencyType CurrencyType { get; set; }
    }
}
