using ExperienceKeeper.Entity.Entities.Base;
using ExperienceKeeper.Entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Entity.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<UserCategory> UserCategories { get; set; }
    }
}
