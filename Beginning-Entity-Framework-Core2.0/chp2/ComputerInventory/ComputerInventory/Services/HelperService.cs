using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerInventory.Services
{
	public static class HelperService
	{

		/**
		 * Centers the Text in the Console
		 */
		public static void WriteHeader(string headerText)
		{
			Console.WriteLine(string.Format("{0," + ((Console.WindowWidth / 2) + headerText.Length / 2) + "}", headerText));
		}

		public static bool ValidateYorN(string entry)
		{
			if (entry.ToLower() == "y" || entry.ToLower() == "n")
			{
				return true;
			}
			return false;
		}
	}
}
