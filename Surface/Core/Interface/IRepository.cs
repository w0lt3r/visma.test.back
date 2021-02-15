using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IRepository<TEntity, in TId> where TEntity : class
    {
        Task<TEntity> GetAsync(TId id);

        //Task<TEntity> GetAsync(TId id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        IQueryable<TEntity> All(bool @readonly = true);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            bool @readonly = true);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);

        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task BatchDeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task BulkDeleteAsync(IEnumerable<TEntity> entities, List<string> updateByProperties = null, bool preserveInsertOrder = false, bool setOutputIdentity = false);
        Task BulkInsertAsync(IEnumerable<TEntity> entities, List<string> updateByProperties = null, bool preserveInsertOrder = false, bool setOutputIdentity = false);
        Task BulkInsertOrUpdateAsync(IEnumerable<TEntity> entities, List<string> updateByProperties = null, bool preserveInsertOrder = false, bool setOutputIdentity = false);
        Task BulkInsertOrUpdateOrDeleteAsync(IEnumerable<TEntity> entities, List<string> updateByProperties = null, bool preserveInsertOrder = false, bool setOutputIdentity = false);
        void RemoveRange(IEnumerable<TEntity> entities);

        void UpdateRange(IEnumerable<TEntity> entities);

        Task<ValidationResult> AddAsync(TEntity entity, params IValidator<TEntity>[] validaciones);

        Task<ValidationResult> AddAsync(TEntity entity, IValidator<TEntity> validation);

        Task<ValidationResult> UpdateAsync(TEntity entity, params IValidator<TEntity>[] validaciones);

        Task<TEntity> AddAsync(TEntity entity);

        Task<ValidationResult> UpdateAsync(TEntity entity, IValidator<TEntity> validation);

        Task<ValidationResult> DeleteAsync(TEntity entity, params IValidator<TEntity>[] validaciones);

        Task<ValidationResult> DeleteAsync(TEntity entity, IValidator<TEntity> validation);

        Task<ValidationResult> ValidateEntityAsync(TEntity entity, IValidator<TEntity> validation);

        Task<ValidationResult> ValidateEntityAsync(TEntity entity, IEnumerable<IValidator<TEntity>> validations);

        Task<ValidationResult> AddEntityAsync(TEntity entity, ValidationResult validationResultEntity);
    }
}
