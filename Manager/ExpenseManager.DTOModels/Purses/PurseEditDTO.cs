using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.DTOModels.Purses
{
    // DTO for transferring information about a purse for editing purposes in EditPursePage
    public class PurseEditDTO
    {
        public Guid Id { get; }
        public string Name { get; }

        public PurseEditDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
