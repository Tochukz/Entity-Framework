using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerInventory.Models
{
	public partial class MachineType
	{
		public MachineType()
		{
			Machine = new HashSet<Machine>();
		}

		public int MachineTypeId { set; get; }

		public string Description { set; get; }

		public ICollection<Machine> Machine { set; get; }
	}
}
