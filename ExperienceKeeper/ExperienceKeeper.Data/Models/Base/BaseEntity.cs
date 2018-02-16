using System;
using System.Collections.Generic;
using System.Text;
using NUlid;

namespace ExperienceKeeper.Data.Models.Base
{
    public class BaseEntity : IBaseEntity, ICreatableEntity, IUpdatableEntity, IRemovableEntity
    {
        public BaseEntity()
        {
            Id = Ulid.NewUlid().ToString();
        }

        public string Id { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }
    }
}
