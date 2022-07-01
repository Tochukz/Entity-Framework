using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookWebsite.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		OjlinksDBContext AppDbContext;

		public IEnumerable<Category> Categories { set; get; }

		public IndexModel(ILogger<IndexModel> logger, OjlinksDBContext context)
		{
			_logger = logger;
			Categories = new List<Category>();
			AppDbContext = context;
		}

		public void OnGet()
		{
			Categories = AppDbContext.Categories.ToList();
		}
	}
}
