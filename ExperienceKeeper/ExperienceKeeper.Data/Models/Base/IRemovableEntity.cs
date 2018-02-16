using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Data.Models.Base
{
    public interface IRemovableEntity
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedOnUtc { get; set; }
    }
}
