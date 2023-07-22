using ArmadaMotors.Domain.Commons;
using ArmadaMotors.Domain.Enums;

namespace ArmadaMotors.Domain.Entities
{
    public class Product : Auditable, ILanguageEntityName, ILanguageEntityDescription
    {
        public string Name { get; set; }
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionUz { get; set; }
        public string DescriptionRu { get; set; }
        public string DescriptionEn { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public ICollection<ProductAsset> Assets { get; set; }
    }
}