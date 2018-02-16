using ExperienceKeeper.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Data.Models
{
    public class RecordUserCategory
    {
        public string RecordId { get; set; }
        public Record Record { get; set; }
        public string UserCategoryId { get; set; }
        public UserCategory UserCategory { get; set; }
    }
}
