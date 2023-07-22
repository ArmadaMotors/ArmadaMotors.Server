using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Shared.Helpers;

namespace ArmadaMotors.Domain.Commons
{
    public interface ILanguageEntityDescription
    {
        public string Description 
        { 
            get 
            {
                if(HttpContextHelper.Language.ToLower() == "ru")
                    return DescriptionRu;
                else if(HttpContextHelper.Language.ToLower() == "en")
                    return DescriptionEn;
                else 
                    return DescriptionUz;
            }
        }
        public string DescriptionUz { get; set; }
        public string DescriptionRu { get; set; }
        public string DescriptionEn { get; set; }
    }
}