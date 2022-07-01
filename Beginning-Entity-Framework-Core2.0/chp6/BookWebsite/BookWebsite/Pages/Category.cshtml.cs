using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookWebsite.Data;

namespace BookWebsite.Pages
{
    public class CategoryModel : PageModel
    {
        public Category Category { set; get; }

        private OjlinksDBContext AppDbContext;

        public CategoryModel(OjlinksDBContext context)
		{
            Category = new Category();
            AppDbContext = context;
		}

        public void OnGet(int categoryId)
        {
            Category category = AppDbContext.Categories
                                            .Where(cat => cat.CategoryId == categoryId)
                                            .Include(cat => cat.Subcategories)
                                            .FirstOrDefault();              
            if (category != null)
			{  
                Category = category;                  
            }
        }
    }
}
