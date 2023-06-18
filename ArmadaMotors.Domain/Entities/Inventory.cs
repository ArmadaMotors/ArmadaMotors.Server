using ArmadaMotors.Domain.Commons;

namespace ArmadaMotors.Domain.Entities
{
    public class Inventory : Auditable
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }    

        public int Amount { get; set; }
    }
}