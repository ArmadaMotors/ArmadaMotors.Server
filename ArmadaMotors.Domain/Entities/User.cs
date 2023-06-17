using ArmadaMotors.Domain.Commons;
using ArmadaMotors.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmadaMotors.Domain.Entities
{
    public class User : Auditable
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [MinLength(5)]
        public string Username { get; set; }

        [MinLength(5)]
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}