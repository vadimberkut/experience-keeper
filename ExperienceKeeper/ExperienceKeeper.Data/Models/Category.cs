using ExperienceKeeper.Data.Models.Base;
using ExperienceKeeper.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Data.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<UserCategory> UserCategories { get; set; }
    }
}
