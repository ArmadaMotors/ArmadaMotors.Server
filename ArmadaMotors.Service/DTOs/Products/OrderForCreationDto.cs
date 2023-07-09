using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArmadaMotors.Service.DTOs.Products
{
    public class OrderForCreationDto
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public long ProductId { get; set; }
    }
}