using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExperienceKeeper.Entity.Entities.Base;
using ExperienceKeeper.Entity.Entities.Identity;

namespace ExperienceKeeper.Entity.Entities
{
    /// <summary>
    /// Records is basic logical item. It represents (general analog of) some note, tutorial, textfile etc.
    /// </summary>
    public class Record : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Name { get; set; }
        public string Content { get; set; }

        public IEnumerable<RecordUserCategory> RecordUserCategories { get; set; }
    }
}
