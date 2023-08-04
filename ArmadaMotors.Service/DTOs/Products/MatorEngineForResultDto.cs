using ArmadaMotors.Domain.Enums;

namespace ArmadaMotors.Service.DTOs.Products;

public class MatorEngineForResultDto
{
	public long  Id { get; set; }
	public string Name { get; set; }
	public Decimal Price { get; set; }
	public long ProductId { get; set; }
    public CurrencyType CurrencyType { get; set; }

}
