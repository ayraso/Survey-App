using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.CachingServices
{
    public interface IMemoryCacheService
    {
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> createItem, TimeSpan cacheDuration);
        void Remove(string key);
    }
}
