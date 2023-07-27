using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Interfaces.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArmadaMotors.Api.Controllers
{
	public class MatorEnginesController : BaseController
	{
		private readonly IMatorEngineService matorEngineService;

		public MatorEnginesController(IMatorEngineService matorEngineService)
		{
			this.matorEngineService = matorEngineService;
		}

		[HttpGet, AllowAnonymous]
		public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
		   => Ok(await matorEngineService.RetrieveAllAsync(@params));

		[HttpGet("{Id}"), AllowAnonymous]
		public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
			=> Ok(await matorEngineService.RetrieveAsync(id));

		[HttpPost]
		public async ValueTask<IActionResult> PostAsync([FromForm] MatorEngineForCreationDto dto)
			=> Ok(await matorEngineService.AddAsync(dto));

		[HttpPut("{Id}")]
		public async ValueTask<IActionResult> PutAsync([FromRoute(Name = "Id")] long id, MatorEngineForCreationDto dto)
			=> Ok(await matorEngineService.ModifyAsync(id, dto));

		[HttpDelete("{Id}")]
		public async ValueTask<IActionResult> DeleteAsync([FromRoute(Name = "Id")] long id)
			=> Ok(await matorEngineService.RemoveAsync(id));

	}
}
