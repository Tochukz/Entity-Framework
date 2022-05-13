using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerInventory.Models
{
	public partial class Machine
	{
		public Machine()
		{
			SupportTicket = new HashSet<SupportTicket>();
		}

		public int MachineId { set; get; }

		public string Name { set; get; }

		public string GeneralRole { set; get; }

		public string InstalledRoles { set; get; }

		public int OperatingSysId { set; get; }

		public int MachineTypeId { set; get; }

		public MachineType MachineType { set; get; }

		public OperatingSys OperatingSys { set; get; }

		public ICollection<SupportTicket> SupportTicket { set; get; }
	}
}
