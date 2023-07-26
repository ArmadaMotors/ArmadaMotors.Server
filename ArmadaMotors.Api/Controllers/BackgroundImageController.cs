using ArmadaMotors.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArmadaMotors.Api.Controllers
{
	public class BackgroundImageController : BaseController
	{
		private readonly IBackgroundImageService backgroundImageService;

		public BackgroundImageController(IBackgroundImageService backgroundImageService)
		{
			this.backgroundImageService = backgroundImageService;
		}

		[HttpPost]
		public async ValueTask<IActionResult> UploadBackroundImage(IFormFile file)
			=> Ok(await this.backgroundImageService.AddAsync(file));

		[HttpDelete("{Id}")]
		public async ValueTask<IActionResult> DelateBackgroundImage(long id)
			=> Ok(await this.backgroundImageService.RemoveAsync(id));
	}
}
