using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Data.Models.Base
{
    public interface ICreatableEntity
    {
        DateTime CreatedOnUtc { get; set; }
    }
}
