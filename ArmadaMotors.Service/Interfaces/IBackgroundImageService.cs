using ArmadaMotors.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ArmadaMotors.Service.Interfaces;

public interface IBackgroundImageService
{
	ValueTask<BackgroundImage> AddAsync(IFormFile file);
	ValueTask<bool> RemoveAsync(long id);
	ValueTask<IEnumerable<BackgroundImage>> RetrieveAllAsync();

}
