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
        Task<IEnumerable<PurseListDTO>> GetPursesAsync();
        Task<PurseDetailsDTO?> GetPurseAsync(Guid id);
        Task<PurseEditDTO?> GetPurseForEditAsync(Guid id);
        Task CreatePurseAsync(PurseCreateDTO createDto);
        Task UpdatePurseAsync(PurseEditDTO editDto);
        Task DeletePurseAsync(Guid id);
    }
}
