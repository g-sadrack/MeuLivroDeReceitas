using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Domain.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    }
}
