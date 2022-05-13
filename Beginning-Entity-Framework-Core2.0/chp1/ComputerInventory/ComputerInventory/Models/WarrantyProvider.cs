using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerInventory.Models
{
	public partial class WarrantyProvider
	{
		public WarrantyProvider()
		{
			MachineWarranty = new HashSet<MachineWarranty>();
		}

		public int WarrantyProviderId { set; get; }

		public string ProviderName { set; get; }

		public int? SupportExtension { set; get; }

		public string SupportNumber { set; get; }

		public ICollection<MachineWarranty> MachineWarranty { set; get; }
	}
}
