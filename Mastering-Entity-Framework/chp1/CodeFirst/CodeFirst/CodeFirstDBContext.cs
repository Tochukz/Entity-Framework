using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CodeFirst.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CodeFirst
{
	public partial class CodeFirstDBContext: DbContext
	{
		public CodeFirstDBContext(): base("name=CodeFirstAppConnStr")
		{
			// You may omit the connection string name and EntityFramework will use the DB Context class name (CodeFirstDBContext) as the default connection string name. 
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

		public DbSet<Todo> Todos { set; get; }
	}
}