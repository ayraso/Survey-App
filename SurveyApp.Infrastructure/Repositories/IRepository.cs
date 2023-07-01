using SurveyApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T?> GetByIdAsync(string id);
        T? GetById(string id);
        Task<IEnumerable<T?>> GetAllAsync();
        IEnumerable<T?> GetAll();
        Task<IEnumerable<T?>> GetAllWithPredicateAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T?> GetAllWithPredicate(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Add(T entity);
        Task UpdateAsync (string id, T entity);
        void Update(string id, T entity);
        Task DeleteByIdAsync(string id);
        void DeleteById(string id);
        Task<bool> IsExistsAsync(string id);
        bool IsExists(string id);
        Task UpdateFieldAsync<TField>(string id, string fieldName, TField value);
        void UpdateField<TField>(string id, string fieldName, TField value);
    }
}
