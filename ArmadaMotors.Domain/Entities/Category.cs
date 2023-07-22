using ArmadaMotors.Domain.Commons;

namespace ArmadaMotors.Domain.Entities
{
    public class Category : Auditable, ILanguageEntityName
    {
        public string Name { get; set; }
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}