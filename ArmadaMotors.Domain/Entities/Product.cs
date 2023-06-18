using ArmadaMotors.Domain.Commons;

namespace ArmadaMotors.Domain.Entities
{
    public class Product : Auditable
    {
        public string Name { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public decimal Price { get; set; }
        public ICollection<ProductAsset> Assets { get; set; }
    }
}