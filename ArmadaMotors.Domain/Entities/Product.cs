using ArmadaMotors.Domain.Commons;
using ArmadaMotors.Domain.Enums;

namespace ArmadaMotors.Domain.Entities
{
    public class Product : Auditable
    {
        public string Name { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public decimal Price { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public ICollection<ProductAsset> Assets { get; set; }
    }
}