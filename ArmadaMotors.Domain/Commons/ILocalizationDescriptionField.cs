using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Shared.Helpers;
using Newtonsoft.Json;

namespace ArmadaMotors.Domain.Commons
{
    public interface ILocalizationDescriptionField
    {
        public string DescriptionUz { get; set; }        
        public string DescriptionRu { get; set; }
        public string DescriptionEn { get; set; }
    }
}