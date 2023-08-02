using ArmadaMotors.Service.Attributes;
using ArmadaMotors.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ArmadaMotors.Api.Controllers
{
	public class AssetsController : BaseController
	{
		private readonly IAssetService assetService;

		public AssetsController(IAssetService assetService)
		{
			this.assetService = assetService;
		}

		[HttpPost("Banner")]
		public async ValueTask<IActionResult> UploadBannerAsset(
			[Required(ErrorMessage = "Please, select file ...")]
			[AllowedExtensions(new string[] { ".jpg", ".png" }, isArray: false)]
			[MaxFileSize(5 * 1024 * 1024, isArray: false)]
			[DataType(DataType.Upload)] IFormFile file)
				=> Ok(await this.assetService.AddBannerAsync(file));

		[HttpDelete("Banners/{Id}")]
		public async ValueTask<IActionResult> DeleteBannerAsset([FromRoute(Name = "Id")] long id)
			=> Ok(await this.assetService.RemoveBannerAsync(id));
		[HttpGet("Banners")]
		public async ValueTask<IActionResult> GetAllBanners()
			=> Ok(await this.assetService.RetrieveAllBanners());

	}
}
