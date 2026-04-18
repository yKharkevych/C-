using System;
using Manager.ExpenseManager.DBModels;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Repositories
{
    // Repository interface for accessing purse data
    public interface IPurseRepository
    {
        Task<IEnumerable<PurseDB>> GetPursesAsync();
        Task<PurseDB?> GetPurseAsync(Guid id);
        Task SavePurseAsync(PurseDB purse);
        Task DeletePurseAsync(Guid id);
    }
}
