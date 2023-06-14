using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArmadaMotors.Service.DTOs.Users
{
    public class UserForCreationDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}