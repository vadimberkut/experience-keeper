using ExperienceKeeper.Entity.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Entity.Entities
{
    public class RecordUserCategory
    {
        public string RecordId { get; set; }
        public Record Record { get; set; }
        public string UserCategoryId { get; set; }
        public UserCategory UserCategory { get; set; }
    }
}
