using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Entity.Entities.Base
{
    public interface ICreatableEntity
    {
        DateTime CreatedOnUtc { get; set; }
    }
}
