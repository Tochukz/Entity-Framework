using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ComputerInventory.Data;
using ComputerInventory.Models;

namespace ComputerInventory.Services
{
	public static class OperatingSysService
	{
		public static void AddOperatingSystem()
		{
			Console.Clear();
			ConsoleKeyInfo cki;
			string result;
			bool cont = false;
			OperatingSys os = new OperatingSys();
			string osName = "";
			do
			{
				HelperService.WriteHeader("Add New Operating System");
				Console.WriteLine("Enter the Name of the Operating System and hit Enter");
				osName = Console.ReadLine();
				if (osName.Length >= 4)
				{
					cont = true;
				}
				else
				{
					Console.WriteLine("Please enter a valid OS name at least 4 characters. \r\nPress any key to continue...");
					Console.ReadKey();
				}

			} while (!cont);

			cont = false;
			os.Name = osName;
			Console.WriteLine("Is the Operating System still supported [y or n]");
			do
			{
				cki = Console.ReadKey();
				result = cki.KeyChar.ToString();
				cont = HelperService.ValidateYorN(result);
			} while (!cont);

			if (result.ToLower() == "y")
			{
				os.StillSupported = true;
			}
			else
			{
				os.StillSupported = false;
			}
			cont = false;

			do
			{
				Console.Clear();
				Console.WriteLine($"Yor entered {os.Name} as the Operating System Name\r\nIs the OS still supported, you entered {os.StillSupported}.\r\nDo you wish to continue? [y or n]");
				cki = Console.ReadKey();
				result = cki.KeyChar.ToString();
				cont = HelperService.ValidateYorN(result);
			} while (!cont);

			if (result.ToLower() == "y")
			{
				bool exists = CheckForExistingOS(os.Name);
				if (exists)
				{
					Console.WriteLine("\r\nOperating System already exists in the database\r\nPress any key to continue...");
					Console.ReadKey();
				}
				else
				{
					using (AppDBContext context = new AppDBContext())
					{
						Console.WriteLine("\r\nAttempting to save changes...");
						context.OperatingSys.Add(os);
						int i = context.SaveChanges();
						if (i == 1)
						{
							Console.WriteLine("Contents Saved\r\nPress any key to continue...");
							Console.ReadKey();
						}
					}
				}
			}
		}

		public static void DisplayOperatingSystems()
		{
			Console.Clear();
			Console.WriteLine("Operating Systems");
			using (AppDBContext context = new AppDBContext())
			{
				foreach (OperatingSys os in context.OperatingSys.ToList())
				{
					//TO learn more, see formating in console applications
					Console.Write($"Name: {os.Name,-39}\tStill Supported = ");
					if (os.StillSupported == true)
					{
						Console.ForegroundColor = ConsoleColor.Green;
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
					}
					Console.WriteLine(os.StillSupported);
					Console.ForegroundColor = ConsoleColor.Blue;
				}

			}
			Console.WriteLine("\r\nPress ant key to continue...");
			Console.ReadKey();
		}

		static void DeleteOperatingSystem(int id)
		{
			OperatingSys os = GetOperatingSystemById(id);
			if (os != null)
			{
				Console.WriteLine($"\r\nAre you sure you want to delete {os.Name}? [y or n]");
				ConsoleKeyInfo cki;
				string result;
				bool cont;
				do
				{
					cki = Console.ReadKey(true);
					result = cki.KeyChar.ToString();
					cont = HelperService.ValidateYorN(result);
				}
				while (!cont);

				if (result.ToLower() == "y")
				{
					Console.WriteLine("\r\nDeleting record");
					using (AppDBContext context = new AppDBContext())
					{
						context.Remove(os);
						context.SaveChanges();
					}
					Console.WriteLine("Record Deleted");
					Console.ReadKey();
				}
				else
				{
					Console.WriteLine("Deleted Aborted\r\nHit any key to continue...");
					Console.ReadKey();
				}
			}
			else
			{
				Console.WriteLine("\r\nOperating System Not Found!");
				Console.ReadKey();
				SelectOperatingSystem("Delete");
			}
		}

		public static OperatingSys GetOperatingSystemById(int id)
		{
			AppDBContext context = new AppDBContext();
			OperatingSys os = context.OperatingSys.FirstOrDefault(item => item.OperatingSysId == id);
			context.Dispose();
			return os;
		}

		public static void SelectOperatingSystem(string operation)
		{
			ConsoleKeyInfo cki;
			Console.Clear();
			HelperService.WriteHeader($"{operation} an Existing Operating System Entry");
			Console.WriteLine($"{"ID",-7}|{"Name",-50}|Still Supported");
			Console.WriteLine("------------------------------------------");
			using (AppDBContext context = new AppDBContext())
			{
				List<OperatingSys> operatingSystems = context.OperatingSys.ToList();
				foreach (OperatingSys os in operatingSystems)
				{
					Console.WriteLine($"{os.OperatingSysId,-7}|{os.Name,-50}|{os.StillSupported}");
				}
			}
			Console.WriteLine($"\r\nEnter the ID of the record you wish to {operation} and hit Enter\r\nYou can hist Esc to exit tis menu");
			bool cont = false;
			string id = "";
			do
			{
				cki = Console.ReadKey(true);
				if (cki.Key == ConsoleKey.Escape)
				{
					cont = true;
				}
				else if (cki.Key == ConsoleKey.Enter)
				{
					if (id.Length > 0)
					{
						cont = true;
					}
					else
					{
						Console.WriteLine("Please enter an ID that is at least 1 digit.");
					}
				}
				else if (cki.Key == ConsoleKey.Backspace)
				{
					Console.Write("\b\b");
					try
					{
						id = id.Substring(0, id.Length - 1);
					}
					catch (ArgumentOutOfRangeException)
					{
						//at th 0 position, can't go any futher back
					}
				}
				else
				{
					if (char.IsNumber(cki.KeyChar))
					{
						id += cki.KeyChar.ToString();
						Console.Write(cki.KeyChar.ToString());
					}
				}
			} while (!cont);

			int osId = Convert.ToInt32(id);
			if ("Delete" == operation)
			{
				DeleteOperatingSystem(osId);
			}
			else if ("Modify" == operation)
			{
				ModifyOperatingSystem(osId);
			}
		}

		


		public static void DeleteAllUnsupportedOperatingSystems()
		{
			using (AppDBContext context = new AppDBContext())
			{
				IQueryable<OperatingSys> os = (from ops in context.OperatingSys
											   where ops.StillSupported == false
											   select ops);
				Console.WriteLine("\r\nDeleting all Unsupported Operating Systems...");
				context.OperatingSys.RemoveRange(os);
				int i = context.SaveChanges();
				Console.WriteLine($"We have deleted {i} records");
				Console.WriteLine("Hit any key to continue...");
				Console.ReadKey();
			}
		}


		public static void ModifyOperatingSystem(int id)
		{
			OperatingSys os = GetOperatingSystemById(id);
			Console.Clear();
			char operation = '0';
			bool cont = false;
			ConsoleKeyInfo cki;
			HelperService.WriteHeader("Update Operating System");
			if (os != null)
			{
				Console.WriteLine($"\r\nOS Name: {os.Name} | Still Supported: {os.StillSupported}");
				Console.WriteLine("To modify the name press 1\r\nTo modify of the OS is still Supported press 2");
				Console.WriteLine("Hit Esc to exit this menu");
				do
				{
					cki = Console.ReadKey(true);
					if (cki.Key == ConsoleKey.Escape)
					{
						cont = true;
					}
					else
					{
						if (char.IsNumber(cki.KeyChar))
						{
							if (cki.KeyChar == '1')
							{
								Console.WriteLine("Updated Operating System Name: ");
								operation = '1';
								cont = true;
							}
							else if (cki.KeyChar == '2')
							{
								Console.WriteLine("Update if the OS is Still Supported [y or n]: ");
								operation = '2';
								cont = true;
							}
						}
					}
				} while (!cont);
			}
			if (operation == '1')
			{
				string osName;
				cont = false;
				do
				{
					osName = Console.ReadLine();
					if (osName.Length >= 4)
					{
						cont = true;
					}
					else
					{
						Console.WriteLine("Please eter a valid OS name of at least 4 characters.\r\nPress anykey to continue...");
						Console.ReadKey();
					}
				} while (!cont);
				os.Name = osName;
			}
			else if (operation == '2')
			{
				string k;
				do
				{
					cki = Console.ReadKey(true);
					k = cki.KeyChar.ToString();
					cont = HelperService.ValidateYorN(k);
				} while (!cont);

				if (k == "y")
				{
					os.StillSupported = true;
				}
				else
				{
					os.StillSupported = false;
				}
			}
			using (AppDBContext context = new AppDBContext())
			{
				OperatingSys ops = context.OperatingSys.FirstOrDefault(i => i.OperatingSysId == os.OperatingSysId);
				if (ops != null)
				{
					ops.Name = os.Name;
					ops.StillSupported = os.StillSupported;
					Console.WriteLine("\r\nUpdated the database...");
					context.SaveChanges();
					Console.WriteLine("Done!\r\nHit any key to continue...");

				}
			}
			Console.ReadKey();
		}

		static bool CheckForExistingOS(string osName)
		{
			bool exists = false;
			using (AppDBContext context = new AppDBContext())
			{
				IQueryable<OperatingSys> result = context.OperatingSys.Where(o => o.Name == osName);
				if (result.Count() > 0)
				{
					exists = true;
				}
			}
			return exists;
		}
	}
}
