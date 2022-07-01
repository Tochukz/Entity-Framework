using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerInventory.Models
{
	public partial class MachineWarranty
	{
		public int MachineWarrantyId { set; get; }

		public string ServiceTag { set; get; }

		public DateTime WarrantyExpiration { set; get; }

		public int MachineId { set; get; }

		public int WarrantyProviderId { set; get; }

		public WarrantyProvider WarrantyProvider { set; get; }
	}
}
