using Core.Interface;
using EFCore.BulkExtensions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
        where TId : IComparable<TId>
    {
        protected MainContext MainContext { get; }
        protected DbSet<TEntity> DbSet { get; set; }
        public Repository(MainContext mainContext)
        {
            MainContext = mainContext;
            DbSet = mainContext.Set<TEntity>();
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var entityTracking = await DbSet.AddAsync(entity);

            return entityTracking.Entity;
        }

        public virtual IQueryable<TEntity> All(bool @readonly = true)
        {
            return @readonly ? DbSet.AsNoTracking() : DbSet;
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.Where(expression).CountAsync();
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            bool @readonly = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (@readonly)
                query = query.AsNoTracking();

            return query.Where(predicate);
        }

        public async Task<TEntity> GetAsync(TId id)
        {
            var keyProperty = MainContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties[0];
            var keyId = (int)Convert.ChangeType(id, typeof(int));

            var dbSet = (IQueryable<TEntity>)DbSet;

            return await dbSet.FirstOrDefaultAsync(x => EF.Property<int>(x, keyProperty.Name) == keyId);
        }

        //public async Task<TEntity> GetAsync(TId id,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        //{
        //    var keyProperty = MainContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties[0];
        //    var keyId = (int)Convert.ChangeType(id, typeof(int));

        //    var dbSet = include != null ? include(DbSet) : (IQueryable<TEntity>)DbSet;

        //    return await dbSet.FirstOrDefaultAsync(x => EF.Property<int>(x, keyProperty.Name) == keyId);
        //}

        public async Task<ValidationResult> AddAsync(TEntity entity, params IValidator<TEntity>[] validaciones)
        {
            var validateEntityResult = await ValidateEntityAsync(entity, validaciones);
            return await AddEntityAsync(entity, validateEntityResult);
        }

        public async Task<ValidationResult> AddAsync(TEntity entity, IValidator<TEntity> validation)
        {
            var validateEntityResult = await ValidateEntityAsync(entity, validation);
            return await AddEntityAsync(entity, validateEntityResult);
        }

        public async Task<ValidationResult> UpdateAsync(TEntity entity, params IValidator<TEntity>[] validaciones)
        {
            var validateEntityResult = await ValidateEntityAsync(entity, validaciones);

            if (validateEntityResult.IsValid)
                MainContext.Update(entity);

            return validateEntityResult;
        }

        public async Task<ValidationResult> UpdateAsync(TEntity entity, IValidator<TEntity> validation)
        {
            var validateEntityResult = await ValidateEntityAsync(entity, validation);

            if (validateEntityResult.IsValid) MainContext.Update(entity);

            return validateEntityResult;
        }

        public async Task<ValidationResult> DeleteAsync(TEntity entity, params IValidator<TEntity>[] validaciones)
        {
            var validateEntityResult = await ValidateEntityAsync(entity, validaciones);

            if (validateEntityResult.IsValid) MainContext.Remove(entity);

            return validateEntityResult;
        }

        public async Task<ValidationResult> DeleteAsync(TEntity entity, IValidator<TEntity> validation)
        {
            var validateEntityResult = await ValidateEntityAsync(entity, validation);

            if (validateEntityResult.IsValid) MainContext.Remove(entity);
            return validateEntityResult;
        }

        public async Task<ValidationResult> ValidateEntityAsync(TEntity entity, IValidator<TEntity> validation)
        {
            if (validation != null)
            {
                var validationResultEntity = await validation.ValidateAsync(entity);
                return validationResultEntity;
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> ValidateEntityAsync(TEntity entity,
            IEnumerable<IValidator<TEntity>> validations)
        {
            if (validations != null)
            {
                var errors = new List<ValidationFailure>();

                foreach (var validation in validations)
                {
                    var currentValidationResult = await validation.ValidateAsync(entity);

                    if (!currentValidationResult.IsValid)
                        errors.AddRange(currentValidationResult.Errors);
                }

                return new ValidationResult(errors);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> AddEntityAsync(TEntity entity, ValidationResult validationResultEntity)
        {
            if (validationResultEntity.IsValid)
                await MainContext.AddAsync(entity);

            return validationResultEntity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }
        //public async Task BulkDeleteAsync(IEnumerable<TEntity> entities)
        //{
        //    await MainContext.BulkDeleteAsync(entities.ToList());
        //}
        public async Task BatchDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await DbSet.Where(predicate).BatchDeleteAsync();

        }
        public async Task BulkDeleteAsync(IEnumerable<TEntity> entities, List<string> updateByProperties = null, bool preserveInsertOrder = false, bool setOutputIdentity = false)
        {
            BulkConfig bulkConfig = new BulkConfig { UpdateByProperties = updateByProperties, PreserveInsertOrder = preserveInsertOrder, SetOutputIdentity = setOutputIdentity };
            await MainContext.BulkDeleteAsync(entities.ToList(), bulkConfig);
        }
        public async Task BulkInsertAsync(IEnumerable<TEntity> entities, List<string> updateByProperties = null, bool preserveInsertOrder = false, bool setOutputIdentity = false)
        {
            BulkConfig bulkConfig = new BulkConfig { UpdateByProperties = updateByProperties, PreserveInsertOrder = preserveInsertOrder, SetOutputIdentity = setOutputIdentity };
            await MainContext.BulkInsertAsync(entities.ToList(), bulkConfig);
        }
        public async Task BulkInsertOrUpdateAsync(IEnumerable<TEntity> entities, List<string> updateByProperties = null, bool preserveInsertOrder = false, bool setOutputIdentity = false)
        {
            BulkConfig bulkConfig = new BulkConfig { UpdateByProperties = updateByProperties, PreserveInsertOrder = preserveInsertOrder, SetOutputIdentity = setOutputIdentity };
            await MainContext.BulkInsertOrUpdateAsync(entities.ToList(), bulkConfig);
        }
        public async Task BulkInsertOrUpdateOrDeleteAsync(IEnumerable<TEntity> entities, List<string> updateByProperties = null, bool preserveInsertOrder = false, bool setOutputIdentity = false)
        {
            BulkConfig bulkConfig = new BulkConfig { UpdateByProperties = updateByProperties, PreserveInsertOrder = preserveInsertOrder, SetOutputIdentity = setOutputIdentity };
            await MainContext.BulkInsertOrUpdateOrDeleteAsync(entities.ToList(), bulkConfig);
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }
        //public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        //    bool @readonly = true)
        //{
        //    IQueryable<TEntity> query = DbSet;

        //    if (@readonly)
        //        query = query.AsNoTracking();

        //    if (include != null)
        //        query = include(query);

        //    return query.Where(predicate);
        //}

        public virtual void Update(TEntity entity)
        {
            if (DbSet.Local.All(p => p != entity))
            {
                DbSet.Attach(entity);
                var entry = MainContext.Entry(entity);
                entry.State = EntityState.Modified;
            }
        }

        public void Delete(TEntity entity)
        {
            DbSet.Attach(entity);

            var entry = MainContext.Entry(entity);
            entry.State = EntityState.Deleted;
        }
    }
}
