using ArmadaMotors.Domain.Commons;
using ArmadaMotors.Domain.Enums;

namespace ArmadaMotors.Domain.Entities;

public class MatorEngine : Auditable
{
	public string Name { get; set; }
	public Decimal Price { get; set; }
	public long ProductId { get; set; }
	public Product Product { get; set; }
	public CurrencyType CurrencyType { get; set; } 
}
