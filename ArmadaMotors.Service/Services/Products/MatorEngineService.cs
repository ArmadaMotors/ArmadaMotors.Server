﻿using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Extensions;
using ArmadaMotors.Service.Interfaces.Products;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ArmadaMotors.Service.Services.Products;

public class MatorEngineService : IMatorEngineService
{
	private readonly IMapper mapper;
	private readonly IRepository<Product> productRepository;
	private readonly IRepository<MatorEngine> matorEngineRepository;

	public MatorEngineService(IMapper mapper, 
		IRepository<Product> productRepository,
		IRepository<MatorEngine> matorEngineRepository)
	{
		this.mapper = mapper;
		this.productRepository = productRepository;
		this.matorEngineRepository = matorEngineRepository;
	}

	public async ValueTask<MatorEngineForResultDto> AddAsync(MatorEngineForCreationDto dto)
	{
		var product = await this.productRepository.SelectByIdAsync(dto.ProductId);
		if (product is null)
			throw new ArmadaException(404, "Product is not found");

		if (product.CurrencyType != dto.CurrencyType)
			throw new ArmadaException(400, "Currency Type is not matched");

		var mappedEngine = this.mapper.Map<MatorEngine>(dto);
		var result = await this.matorEngineRepository.InsertAsync(mappedEngine);

		return this.mapper.Map<MatorEngineForResultDto>(result);
	}

	public async ValueTask<bool> RemoveAsync(long id)
	{
		var result = await this.matorEngineRepository.DeleteAsync(id);
		if (!result)
			throw new ArmadaException(404, "Engine is not found");
		await this.matorEngineRepository.SaveChangesAsync();
		return result;
	}

	public async ValueTask<IEnumerable<MatorEngineForResultDto>> RetrieveAllAsync(PaginationParams @params)
	{
		var engines = await this.matorEngineRepository.SelectAll()
			.ToPagedList(@params)
			.ToListAsync();

		return this.mapper.Map<IEnumerable<MatorEngineForResultDto>>(engines);
	}

	public async ValueTask<IEnumerable<MatorEngineForResultDto>> RetrieveAllByProductIdAsync(long productId)
	{
		var engines = await this.matorEngineRepository.SelectAll()
			.Where(me => me.ProductId == productId)
			.ToListAsync();

		return this.mapper.Map<IEnumerable<MatorEngineForResultDto>>(engines);
	}

	public async ValueTask<MatorEngineForResultDto> ModifyAsync(long id, MatorEngineForCreationDto dto)
	{
		var engine = await this.matorEngineRepository.SelectByIdAsync(id);
		var product = await this.productRepository.SelectByIdAsync(dto.ProductId);
		if (engine is null)
			throw new ArmadaException(404, "Engine is not found");
		if (product.CurrencyType != dto.CurrencyType)
			throw new ArmadaException(400, "Currency Type is not matched");

		var mappedEngine = this.mapper.Map(dto,engine);
		mappedEngine.UpdatedAt = DateTime.UtcNow;
		await this.matorEngineRepository.SaveChangesAsync();

		return this.mapper.Map<MatorEngineForResultDto>(engine);
	}
}
