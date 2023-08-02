using Microsoft.AspNetCore.Http;
using ArmadaMotors.Shared.Helpers;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Interfaces;
using SixLabors.ImageSharp.Formats.Png;
using ArmadaMotors.Domain.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ArmadaMotors.Service.Services
{
	public class AssetService : IAssetService
	{
		private readonly IRepository<Asset> _assetRepository;
		private readonly IRepository<BannerAsset> _bannerAssetRepository;
		public AssetService(IRepository<Asset> assetRepository, IRepository<BannerAsset> bannerAssetRepository)
		{
			_assetRepository = assetRepository;
			_bannerAssetRepository = bannerAssetRepository;
		}

		public async ValueTask<Asset> AddAsync(IFormFile file)
		{
			string rootPath = EnvironmentHelper.WebRootPath;
			string fileName = Guid.NewGuid().ToString("N") + file.FileName;
			string path = Path.Combine(rootPath, "assets", fileName);

			// reduce file
			var reducedFile = await ReduceImageSizeAsync(file.OpenReadStream());

			using (var fileStream = File.OpenWrite(path))
			{
				await reducedFile.CopyToAsync(fileStream);
			}

			return await this._assetRepository.InsertAsync(new Asset
			{
				Url = Path.Combine("assets", fileName)
			});
		}

		public async ValueTask<bool> IsValidAspectRatio(IFormFile file)
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
			double allowedAspectRatio = 1920.0 / 710.0 ;

			// Check if the aspect ratio is within the allowed range

			return Math.Abs(aspectRatio - allowedAspectRatio) <= 0.3;
		}
		public async ValueTask<BannerAsset> AddBannerAsync(IFormFile file)
		{
			var isValidRatio = await IsValidAspectRatio(file);
			if (!isValidRatio)
				throw new ArmadaException(400, "Invalid aspect ratio. Allowed ratios are 16:9 and 4:3.");

			string rootPath = EnvironmentHelper.WebRootPath;
			string fileName = Guid.NewGuid().ToString("N") + file.FileName;
			string path = Path.Combine(rootPath, "assets", fileName);

			// reduce file
			var reducedFile = await ReduceImageSizeAsync(file.OpenReadStream());

			using (var fileStream = File.OpenWrite(path))
			{
				await reducedFile.CopyToAsync(fileStream);
			}

			var asset = await this._assetRepository.InsertAsync(new Asset
			{
				Url = Path.Combine("assets", fileName)
			});

			return await this._bannerAssetRepository.InsertAsync(new BannerAsset
			{
				AssetId = asset.Id
			});
		}

		public async ValueTask<MemoryStream> ReduceImageSizeAsync(Stream file)
		{
			if (file == null || file.Length == 0)
				throw new ArmadaException(400, "No file was uploaded.");

			using (var image = await Image.LoadAsync(file))
			{
				// Resize the image to your desired dimensions while preserving the aspect ratio
				int maxWidth = 1000; // Change this value as per your requirements
				int maxHeight = 1000; // Change this value as per your requirements
				image.Mutate(x => x.Resize(new ResizeOptions
				{
					Size = new Size(maxWidth, maxHeight),
					Mode = ResizeMode.Max
				}));

				// Compress the image to reduce the file size
				var outputStream = new MemoryStream();
				var encoder = new PngEncoder()
				{
					CompressionLevel = PngCompressionLevel.BestCompression,
					ColorType = PngColorType.RgbWithAlpha
				};

				await image.SaveAsync(outputStream, encoder);

				// Do something with the compressed image, such as saving it to disk or uploading to storage
				outputStream.Seek(0, SeekOrigin.Begin);
				return outputStream; // Return the compressed image as a response
			}
		}

		public ValueTask<bool> RemoveAsync(long id)
		{
			throw new NotImplementedException();
		}

		public async ValueTask<bool> RemoveBannerAsync(long id)
		{
			var asset = await this._bannerAssetRepository.SelectAll()
				.Include(ba => ba.Asset)
				.FirstOrDefaultAsync(ba => ba.Id == id);
			if (asset == null)
				throw new ArmadaException(404, "Background image not found");

			// Delete the image file from the disk
			string rootPath = EnvironmentHelper.WebRootPath;
			string imagePath = Path.Combine(rootPath, asset?.Asset?.Url);
			if (File.Exists(imagePath))
				File.Delete(imagePath);

			return await this._bannerAssetRepository.DeleteAsync(id);
		}

		public ValueTask<IEnumerable<Asset>> RetrieveAllAsync(PaginationParams @params)
		{
			throw new NotImplementedException();
		}

		public ValueTask<Asset> RetrieveById(long id)
		{
			throw new NotImplementedException();
		}

		public async ValueTask<IEnumerable<BannerAsset>> RetrieveAllBanners()
			=> await this._bannerAssetRepository.SelectAll()
				.Include(a => a.Asset).ToListAsync();
	}
}
