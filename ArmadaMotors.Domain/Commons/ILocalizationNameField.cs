using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Shared.Helpers;
using Newtonsoft.Json;

namespace ArmadaMotors.Domain.Commons
{
    public interface ILocalizationNameField
    {
        [JsonIgnore]
        public string NameUz { get; set; }
        
        [JsonIgnore]
        public string NameRu { get; set; }
        
        [JsonIgnore]
        public string NameEn { get; set; }
    }
}