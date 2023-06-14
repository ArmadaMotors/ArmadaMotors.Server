using ArmadaMotors.Domain.Commons;
using ArmadaMotors.Domain.Enums;

namespace ArmadaMotors.Domain.Entities
{
    public class User : Auditable
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}