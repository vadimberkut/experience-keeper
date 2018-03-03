using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Entity.Entities.Base
{
    public interface IRemovableEntity
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedOnUtc { get; set; }
    }
}
