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
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
    }
}