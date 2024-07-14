namespace AssessmentSystem.Service.API.Services.RedisCacheService
{
    public interface IRedisCache
    {
        Task<byte[]?> Get(string key);
        Task Add(string key, string value);
    }
}
