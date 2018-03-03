using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Entity.Entities.Base
{
    public interface IUpdatableEntity
    {
        DateTime UpdatedOnUtc { get; set; }
    }
}
