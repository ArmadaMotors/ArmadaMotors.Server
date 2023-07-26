using ArmadaMotors.Domain.Commons;

namespace ArmadaMotors.Domain.Entities;

public class BackgroundImage : Auditable
{
	public string Url { get; set; }
}
