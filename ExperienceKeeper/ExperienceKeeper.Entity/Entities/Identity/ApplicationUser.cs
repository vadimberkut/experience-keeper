using System;
using System.Collections.Generic;
using System.Text;
using ExperienceKeeper.Entity.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace ExperienceKeeper.Entity.Entities.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser :
                                    IdentityUser,
                                    IBaseEntity, 
                                    ICreatableEntity, 
                                    IUpdatableEntity, 
                                    IRemovableEntity
    {
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }

        //public string DisplayName { get; set; }
   

        public IEnumerable<UserCategory> UserCategories { get; set; }
        public IEnumerable<Record> ExperienceRecords { get; set; }
    }
}
