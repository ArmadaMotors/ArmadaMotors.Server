using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.Interfaces;
using ArmadaMotors.Shared.Helpers;
using Microsoft.AspNetCore.Http;

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

            using(var fileStream = File.OpenWrite(path))
            {
                await file.CopyToAsync(fileStream);
            }

            return await this._assetRepository.InsertAsync(new Asset
            {
                Url = Path.Combine("assets", fileName)
            });
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
