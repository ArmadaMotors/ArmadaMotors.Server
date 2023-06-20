using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Interfaces;
using ArmadaMotors.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace ArmadaMotors.Service.Services
{
    public class AssetService : IAssetService
    {
        private readonly IRepository<Asset> _assetRepository;
        public AssetService(IRepository<Asset> assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async ValueTask<Asset> AddAsync(IFormFile file)
        {
            string rootPath = EnvironmentHelper.WebRootPath;
            string fileName = Guid.NewGuid().ToString("N") + file.FileName;
            string path = Path.Combine(rootPath, "assets", fileName);

            // reduce file
            var reducedFile = await ReduceImageSizeAsync(file.OpenReadStream());

            using(var fileStream = File.OpenWrite(path))
            {
                await reducedFile.CopyToAsync(fileStream);
            }

            return await this._assetRepository.InsertAsync(new Asset
            {
                Url = Path.Combine("assets", fileName)
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

        public ValueTask<IEnumerable<Asset>> RetrieveAllAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Asset> RetrieveById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
