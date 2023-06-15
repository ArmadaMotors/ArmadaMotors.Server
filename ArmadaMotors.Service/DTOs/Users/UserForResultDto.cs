using ArmadaMotors.Domain.Commons;
using ArmadaMotors.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaMotors.Service.DTOs.Users
{
    public class UserForResultDto : Auditable
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; }
    }
}
