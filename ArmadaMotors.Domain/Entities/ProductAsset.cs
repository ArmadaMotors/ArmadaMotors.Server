using ArmadaMotors.Domain.Commons;

namespace ArmadaMotors.Domain.Entities
{
    public class ProductAsset : Auditable
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}