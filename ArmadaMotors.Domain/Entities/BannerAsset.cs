using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Domain.Commons;

namespace ArmadaMotors.Domain.Entities
{
    public class BannerAsset : Auditable
    {
        public long AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}