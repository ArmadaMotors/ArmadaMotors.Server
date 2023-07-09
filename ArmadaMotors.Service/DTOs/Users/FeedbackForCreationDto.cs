using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArmadaMotors.Service.DTOs.Users
{
    public class FeedbackForCreationDto
    {
        public long ProductId { get; set; }
        public string Message { get; set; }
    }
}