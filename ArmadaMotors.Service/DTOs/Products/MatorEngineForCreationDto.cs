using ArmadaMotors.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmadaMotors.Service.DTOs.Products;

public class MatorEngineForCreationDto
{
	public string Name { get; set; }
	public Decimal Price { get; set; }

	[Required(ErrorMessage = "You must enter the ProductId")]
	public long ProductId { get; set; }
    public CurrencyType CurrencyType { get; set; }

}
