using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExperienceKeeper.Data.Models.Base;
using ExperienceKeeper.Data.Models.Identity;

namespace ExperienceKeeper.Data.Models
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
