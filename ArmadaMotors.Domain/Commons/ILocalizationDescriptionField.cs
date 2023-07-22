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
        [JsonIgnore]
        public string DescriptionUz { get; set; }
        
        [JsonIgnore]
        public string DescriptionRu { get; set; }
        
        [JsonIgnore]
        public string DescriptionEn { get; set; }
    }
}