using ArmadaMotors.Domain.Commons;
using ArmadaMotors.Shared.Helpers;

namespace ArmadaMotors.Domain.Entities
{
    public class Category : Auditable, ILocalizationNameField
    {
        public string Name { get; set; }
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }

        public long? ParentId { get; set; }
        public Category Parent { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}