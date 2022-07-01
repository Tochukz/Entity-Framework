using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerInventory.Models
{
	public partial class SupportLog
	{
		public int SupportLogId { set; get; }

		public DateTime SupportLogEntryDate { set; get; }

		public string SupportLogEntry { set; get; }

		public string SupportLogUpdatedBy { set; get; }

		public int SupportTicketId { set; get; }

		public SupportTicket SupportTicket { set; get; }
	}
}
