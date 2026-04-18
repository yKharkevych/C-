using Manager.ExpenseManager.DBModels;
using Manager.ExpenseManager.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Repositories
{
    // Repository implementation for accessing purse data
    public class PurseRepository : IPurseRepository
    {
        private readonly IStorageContext _storageContext;

        public PurseRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public Task<IEnumerable<PurseDB>> GetPursesAsync()
        {
            return _storageContext.GetPursesAsync();
        }

        public Task<PurseDB?> GetPurseAsync(Guid id)
        {
            return _storageContext.GetPurseAsync(id);
        }

        public Task SavePurseAsync(PurseDB purse)
        {
            return _storageContext.SavePurseAsync(purse);
        }

        public Task DeletePurseAsync(Guid id)
        {
            return _storageContext.DeletePurseAsync(id);
        }
    }
}
