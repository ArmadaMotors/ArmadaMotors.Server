using ArmadaMotors.Domain.Commons;
using ArmadaMotors.Domain.Enums;

namespace ArmadaMotors.Domain.Entities
{
    public class Order : Auditable
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public long ProductId { get; set; }
        public OrderStatus Status { get; set; }
    }
}