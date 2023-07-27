using ArmadaMotors.Domain.Commons;

namespace ArmadaMotors.Domain.Entities;

public class MatorEngine : Auditable
{
	public string Name { get; set; }
	public Decimal Price { get; set; }
	public long ProductId { get; set; }
	public Product Product { get; set; }
}
