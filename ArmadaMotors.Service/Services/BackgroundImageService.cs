﻿using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Interfaces;
using ArmadaMotors.Shared.Helpers;
using Microsoft.AspNetCore.Http;

namespace ArmadaMotors.Service.Services;

public class BackgroundImageService : IBackgroundImageService
{
	private readonly IRepository<BackgroundImage> backgrounimageRepository;
	private readonly IAssetService assetService;

	public BackgroundImageService(IRepository<BackgroundImage> backgrounimageRepository, IAssetService assetService)
	{
		this.backgrounimageRepository = backgrounimageRepository;
		this.assetService = assetService;
	}

	public async ValueTask<BackgroundImage> AddAsync(IFormFile file)
	{
		if (file == null || file.Length == 0)
			throw new ArmadaException(400, "No file was uploaded.");

		// Calculate the aspect ratio of the image
		double aspectRatio;
		using (var image = Image.Load(file.OpenReadStream()))
		{
			aspectRatio = (double)image.Width / image.Height;
		}

		// Define your desired aspect ratios here (e.g., 16:9 and 4:3)
		double[] allowedAspectRatios = { 16.0 / 9.0, 4.0 / 3.0 };

		// Check if the aspect ratio is within the allowed range
		bool isValidAspectRatio = false;
		foreach (var ratio in allowedAspectRatios)
		{
			if (Math.Abs(aspectRatio - ratio) < 0.01) // You can adjust the tolerance as needed
			{
				isValidAspectRatio = true;
				break;
			}
		}

		if (!isValidAspectRatio)
		{
			throw new ArmadaException(400, "Invalid aspect ratio. Allowed ratios are 16:9 and 4:3.");
		}

		string rootPath = EnvironmentHelper.WebRootPath;
		string fileName = Guid.NewGuid().ToString("N") + file.FileName;
		string path = Path.Combine(rootPath, "BackgroundImages", fileName);

		// reduce file
		var reducedFile = await this.assetService.ReduceImageSizeAsync(file.OpenReadStream());

		using (var stream = new FileStream(path, FileMode.Create))
		{
			await reducedFile.CopyToAsync(stream);
		}

		// Save the image URL in the database
		var backgroundImage = await this.backgrounimageRepository.InsertAsync(new BackgroundImage
		{
			Url = Path.Combine("backgrounds", fileName)
		});

		return backgroundImage;
	}


	public async ValueTask<bool> RemoveAsync(long id)
	{
		var backgroundImage = await this.backgrounimageRepository.SelectByIdAsync(id);
		if (backgroundImage == null)
			throw new ArmadaException(404, "Background image not found");

		// Delete the image file from the disk
		string rootPath = EnvironmentHelper.WebRootPath;
		string imagePath = Path.Combine(rootPath, backgroundImage.Url);
		if (File.Exists(imagePath))
			File.Delete(imagePath);

		return await this.backgrounimageRepository.DeleteAsync(id);
	}
}
