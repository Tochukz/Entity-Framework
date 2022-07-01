using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookWebsite.Data;

namespace BookWebsite.Pages
{
    public class BookModel : PageModel
    {
        public Book Book { set; get; }

        private OjlinksDBContext AppDbContext;

        public BookModel(OjlinksDBContext context)
		{
            Book = new Book();
            AppDbContext = context;
		}

        public void OnGet(int bookId)
        {
            
            Book book = AppDbContext.Books
                                    .Where(bk => bk.BookId == bookId)
                                    .Include(bk => bk.Subcategory)
                                    .ThenInclude(sub => sub.Category)
                                    .FirstOrDefault();
            if (book != null)
			{
                Book = book;
			}
        }
    }
}
