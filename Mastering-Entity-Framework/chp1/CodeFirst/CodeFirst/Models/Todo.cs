using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Models
{
	//[Table("Todos")]
	public class Todo
	{
		//[Key]
		public int Id { set; get; }

		// [Column("TodoItem", TypeName = "ntext")]
		public string TodoItem { set; get; }

		// [Column("IsDone", TypeName = "bit")]
		public bool IsDone { set; get; }
	}
}