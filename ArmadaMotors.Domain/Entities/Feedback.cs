using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Domain.Commons;

namespace ArmadaMotors.Domain.Entities
{
    public class Feedback : Auditable
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public bool IsAvailable { get; set; }
        public string Message { get; set; }

        public long? UserId { get; set; }
        public User User { get; set; }
    }
}