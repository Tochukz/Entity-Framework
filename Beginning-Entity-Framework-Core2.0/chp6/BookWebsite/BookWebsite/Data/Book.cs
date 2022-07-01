using System;
using System.Collections.Generic;

#nullable disable

namespace BookWebsite.Data
{
    public partial class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int? SubcategoryId { get; set; }
        public string Author { get; set; }
        public string Edition { get; set; }
        public double? Price { get; set; }
        public string Img { get; set; }
        public int? Availability { get; set; }
        public string Details { get; set; }
        public int? Pages { get; set; }
        public string Language { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Subcategory Subcategory { get; set; }
    }
}
