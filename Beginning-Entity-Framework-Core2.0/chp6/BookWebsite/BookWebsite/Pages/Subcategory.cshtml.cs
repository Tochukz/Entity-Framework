using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookWebsite.Data;

namespace BookWebsite.Pages
{
    public class SubcategoryModel : PageModel
    {
        public Subcategory Subcategory { set; get; }

        private OjlinksDBContext AppDbContext;

        public SubcategoryModel(OjlinksDBContext context)
		{
            Subcategory = new Subcategory();
            AppDbContext = context;
		}

        public void OnGet(int subcategoryId)
        {
     
            Subcategory subcategory = AppDbContext.Subcategories
                                                .Where(sub => sub.SubcategoryId == subcategoryId)
                                                .Include(sub => sub.Books)
                                                .FirstOrDefault();
            if (subcategory != null)
			{
                Subcategory = subcategory;
			}
        }
    }
}
