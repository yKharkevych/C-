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

        public IEnumerable<PurseDB> GetPurses()
        {
            return _storageContext.GetPurses();
        }

        public PurseDB GetPurse(Guid id)
        {
            return _storageContext.GetPurse(id);
        }
    }
}
