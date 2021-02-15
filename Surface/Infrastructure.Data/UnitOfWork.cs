using Core.Common;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainContext _mainContext;
        public UnitOfWork(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public Task SaveChangesAsync()
        {
            return _mainContext.SaveChangesAsync();
        }
        public Task SaveChangesAsync(int user)
        {
            _mainContext.ChangeTracker.DetectChanges();
            var added = _mainContext.ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Added)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in added)
            {
                //var property = entity.GetType().GetProperties().ToList().Find(x => x.Name == "Id");
                //var type = typeof(AuditableEntity<>).MakeGenericType(property.GetType());
                //var instance = Activator.CreateInstance(type);

                if (entity is AuditableEntity<int> || entity is AuditableEntity<string>)
                {
                    var type = entity.GetType();
                    type.GetProperty("CreatedOn").SetValue(entity, DateTimeHelper.GetCurrentPeruvianDateTime());
                    type.GetProperty("CreatedBy").SetValue(entity, user);
                }
            }

            var modified = _mainContext.ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Modified)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in modified)
            {
                if (entity is AuditableEntity<int> || entity is AuditableEntity<string>)
                {
                    var type = entity.GetType();
                    type.GetProperty("ModifiedOn").SetValue(entity, DateTimeHelper.GetCurrentPeruvianDateTime());
                    type.GetProperty("ModifiedBy").SetValue(entity, user);
                }
            }
            return _mainContext.SaveChangesAsync();
        }
    }
}
