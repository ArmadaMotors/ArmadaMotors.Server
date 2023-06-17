using ArmadaMotors.Domain.Commons;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Shared.Helpers;
using Newtonsoft.Json;

namespace ArmadaMotors.Service.Extensions
{
    public static class CollectionExtensions
    {
        public static IQueryable<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, PaginationParams @params)
            where TEntity : Auditable
        {
            var metaData = new PaginationMetadata(entities.Count(), @params);

            var json = JsonConvert.SerializeObject(metaData);

            if (HttpContextHelper.ResponseHeaders != null)
            {
                if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
                    HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

                HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);
            }

            return @params.PageIndex > 0 && @params.PageSize > 0 ?
                entities.OrderBy(e => e.Id)
                    .Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize) :
                        throw new ArmadaException(400, "Please, enter valid numbers");
        }
    }
}
