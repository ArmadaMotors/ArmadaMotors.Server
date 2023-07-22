using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Shared.Helpers;

namespace ArmadaMotors.Domain.Commons
{
    public interface ILanguageEntityName
    {
        public string Name 
        { 
            get 
            {
                if(HttpContextHelper.Language.ToLower() == "ru")
                    return NameRu;
                else if(HttpContextHelper.Language.ToLower() == "en")
                    return NameEn;
                else 
                    return NameUz;
            }
        }
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
    }
}