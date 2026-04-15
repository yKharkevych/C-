using Manager.ExpenseManager.DBModels;
using Manager.ExpenseManager.DTOModels.Purses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Services
{
    // Service interface for managing purses
    public interface IPurseService
    {
        IEnumerable<PurseListDTO> GetPurses();
        PurseDetailsDTO GetPurse(Guid id);
    }
}
