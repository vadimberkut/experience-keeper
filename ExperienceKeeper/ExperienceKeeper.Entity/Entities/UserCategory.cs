using ExperienceKeeper.Entity.Entities.Base;
using ExperienceKeeper.Entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Entity.Entities
{
    public class UserCategory : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public IEnumerable<RecordUserCategory> RecordUserCategories { get; set; }
    }
}
