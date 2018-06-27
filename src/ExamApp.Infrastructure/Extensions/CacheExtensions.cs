using System;
using ExamApp.Infrastructure.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace ExamApp.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static void SetJwt(this IMemoryCache cache, Guid tokenId, JwtDto jwt)
            => cache.Set(GetJwtKey(tokenId), jwt, TimeSpan.FromSeconds(60));
        public static JwtDto GetJwt(this IMemoryCache cache, Guid tokenId)
            => cache.Get<JwtDto>(GetJwtKey(tokenId));
        public static string GetJwtKey(Guid tokenId)
            => $"{tokenId}-jwt";
    }
}