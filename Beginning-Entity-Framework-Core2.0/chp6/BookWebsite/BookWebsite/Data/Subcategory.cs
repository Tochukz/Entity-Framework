using System;
using System.Collections.Generic;

#nullable disable

namespace BookWebsite.Data
{
    public partial class Subcategory
    {
        public Subcategory()
        {
            Books = new HashSet<Book>();
        }

        public int SubcategoryId { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
