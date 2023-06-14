using ArmadaMotors.Domain.Commons;

namespace ArmadaMotors.Domain.Entities
{
    public class Asset : Auditable
    {
        public string Url { get; set; }
    }
}