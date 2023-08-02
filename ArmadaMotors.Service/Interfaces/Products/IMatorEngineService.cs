using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Products;

namespace ArmadaMotors.Service.Interfaces.Products;

public interface IMatorEngineService
{
	ValueTask<MatorEngineForResultDto> AddAsync(MatorEngineForCreationDto dto);
	ValueTask<MatorEngineForResultDto> ModifyAsync(long id, MatorEngineForCreationDto dto);
	ValueTask<bool> RemoveAsync(long id);
	ValueTask<MatorEngineForResultDto> RetrieveAsync(long id);
	ValueTask<IEnumerable<MatorEngineForResultDto
		>> RetrieveAllAsync(PaginationParams @params);
}
