using System;
using Manager.ExpenseManager.DBModels;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Repositories
{
    // Repository interface for accessing purse data
    public interface IPurseRepository
    {
        IEnumerable<PurseDB> GetPurses();
        PurseDB GetPurse(Guid id);
    }
}
