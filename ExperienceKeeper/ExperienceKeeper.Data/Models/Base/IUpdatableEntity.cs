﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Data.Models.Base
{
    public interface IUpdatableEntity
    {
        DateTime UpdatedOnUtc { get; set; }
    }
}
