using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerInventory.Models
{
	public partial class SupportTicket
	{
		public SupportTicket()
		{
			SupportLog = new HashSet<SupportLog>();
		}

		public int SupportTicketId { set; get; }

		public DateTime DateReported { set; get; }

		public DateTime? DateResolved { set; get; }

		public string IssueDescription { set; get; }

		public string IssueDetails { set; get; }

		public string TicketOpenedBy { set; get; }

		public int MachineId { set; get; }

		public Machine Machine { set; get; }

		public ICollection<SupportLog> SupportLog { set; get; }
	}
}
