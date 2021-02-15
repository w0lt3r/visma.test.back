using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        Task SaveChangesAsync(int user);
    }
}
