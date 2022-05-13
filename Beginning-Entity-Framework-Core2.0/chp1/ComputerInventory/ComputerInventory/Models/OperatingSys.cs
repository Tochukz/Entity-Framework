using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerInventory.Models
{
	public partial class OperatingSys
	{
		public OperatingSys()
		{
			Machine = new HashSet<Machine>();
		}

		public int OperatingSysId { set; get; }

		public string Name { set; get; }

		public bool StillSupported { set; get; }

		public ICollection<Machine> Machine { set; get; }
	}
}
