using System;
using System.Collections.Generic;

#nullable disable

namespace BookWebsite.Data
{
    public partial class Category
    {
        public Category()
        {
            Subcategories = new HashSet<Subcategory>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}
