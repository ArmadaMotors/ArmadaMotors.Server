using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Domain.Commons;

namespace ArmadaMotors.Service.DTOs.Products
{
    public class CategoryForCreationDto : ILocalizationNameField
    {
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public long? ParentId { get; set; }
    }
}