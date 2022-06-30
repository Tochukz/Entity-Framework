using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ComputerInventory.Models;
using ComputerInventory.Data;

namespace ComputerInventory
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			int result = -1;
			while(result != 9)
			{
				result = MainMenu();
			}
		}

		static int MainMenu()
		{
			int result = -1;
			ConsoleKeyInfo cki;
			bool cont = false;
			do
			{
				Console.Clear();
				WriteHeader("Welcome to Newbiew Data Systems");
				WriteHeader("Main Menu");
				Console.WriteLine("\r\nPlease select from the list below for what you would like to do today");
				Console.WriteLine("1. List Machines in Inventory");
				Console.WriteLine("2. List All Operating Systems");
				Console.WriteLine("3. Data Entry Menu");
				Console.WriteLine("4. Data Modification Menu");
				Console.WriteLine("9. Exit");
				cki = Console.ReadKey();
				try
				{
					result = Convert.ToInt16(cki.KeyChar.ToString());
					if (result == 1)
					{
						// DisplayAllMachines();
					}
					else if (result == 2)
					{
						DisplayOperatingSystems();
					}
					else if(result == 3)
					{
						DataEntryMenu();
					}
					else if (result == 4)
					{
						DataModificationMenu();
					}
					else if (result == 9)
					{  // We are existing so nothing to do 
						cont = true;
					}
				}
				catch(System.FormatException)
				{
					// a key that wasn't a number
				}
			} while (!cont);

			return result;
		}

		static void DataEntryMenu()
		{
			ConsoleKeyInfo cki;
			int result = -1;
			bool cont = false;
			do
			{
				Console.Clear();
				WriteHeader("Data Entry Menu");
				Console.WriteLine("\r\nPlease select from the list below for what you would like to do today.");
				Console.WriteLine("1. Add a New Machine");
				Console.WriteLine("2. Add a New Operating System");
				Console.WriteLine("3. Add a New Warranty Provider");
				Console.WriteLine("9. Exit Menu");
				cki = Console.ReadKey();
				try
				{
					result = Convert.ToInt16(cki.KeyChar.ToString());
					if (result == 1)
					{
						//AddMachine();
					}
					else if (result == 2)
					{
						AddOperatingSystem();
					}
					else if (result == 3)
					{
						// AddNewWarrantyProvider();
					}
					else if (result == 9)
					{
						// we are exiting so nothing to do 
						cont = true;
					}
				}
				catch(System.FormatException)
				{
					// a key that wasn't a number 
				}
			} while (!cont);
		}

		/**
		 * Centers the Text in the Console
		 */ 
		static void WriteHeader(string headerText)
		{
			Console.WriteLine(string.Format("{0," + ((Console.WindowWidth / 2) + headerText.Length / 2) + "}", headerText));
		}

		static bool ValidateYorN(string entry)
		{
			if (entry.ToLower() == "y" || entry.ToLower() == "n")
			{
				return true;
			}
			return false;
		}

		static bool CheckForExistingOS(string osName)
		{
			bool exists = false;
			using(AppDBContext context = new AppDBContext())
			{
				IQueryable<OperatingSys> result =  context.OperatingSys.Where(o => o.Name == osName);
				if (result.Count() > 0)
				{
					exists = true;
				}
			}
			return exists;
		}

		static void AddOperatingSystem()
		{
			Console.Clear();
			ConsoleKeyInfo cki;
			string result;
			bool cont = false;
			OperatingSys os = new OperatingSys();
			string osName = "";
			do
			{
				WriteHeader("Add New Operating System");
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
				cont = ValidateYorN(result);
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
				cont = ValidateYorN(result);
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
	
	    static void DisplayOperatingSystems()
		{
			Console.Clear();
			Console.WriteLine("Operating Systems");
			using (AppDBContext context = new AppDBContext())
			{
				foreach(OperatingSys os in context.OperatingSys.ToList())
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
					cont = ValidateYorN(result);
				}
				while (!cont);

				if (result.ToLower() == "y")
				{
					Console.WriteLine("\r\nDeleting record");
					using(AppDBContext context = new AppDBContext())
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
	
	    static OperatingSys GetOperatingSystemById(int id)
		{
			AppDBContext context = new AppDBContext();
			OperatingSys os = context.OperatingSys.FirstOrDefault(item => item.OperatingSysId == id);
			context.Dispose();
			return os;
		}

		static void SelectOperatingSystem(string operation)
		{
			ConsoleKeyInfo cki;
			Console.Clear();
			WriteHeader($"{operation} an Existing Operating System Entry"); 
			Console.WriteLine($"{"ID",-7}|{"Name",-50}|Still Supported");
			Console.WriteLine("------------------------------------------");
			using (AppDBContext context = new AppDBContext())
			{
				List<OperatingSys> operatingSystems = context.OperatingSys.ToList();
				foreach(OperatingSys os in operatingSystems)
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
;			}
		}
	
	    static void DataModificationMenu()
		{
			ConsoleKeyInfo cki;
			int result = -1;
			bool cont = false;
			do
			{
				Console.Clear();
				WriteHeader("Data Modification Menu");
				Console.WriteLine("\r\nPlease select from the list below for what you would like to do today");
				Console.WriteLine("1. Delete Operating System");
				Console.WriteLine("2. Modify Operating System");
				Console.WriteLine("3. Delete All Unsupported Operating Systems");
				Console.WriteLine("9. Exit Menu");
				cki = Console.ReadKey();
				try
				{
					result = Convert.ToInt16(cki.KeyChar.ToString());
					if (result == 1)
					{
						SelectOperatingSystem("Delete");
					}
					else if (result == 2)
					{
						SelectOperatingSystem("Modify");
					}
					else if (result == 3)
					{
						DeleteAllUnsupportedOperatingSystems();
					}
					else if (result == 9)
					{
						// We are exiting so nothing to do
						cont = true;
					}
				}
				catch (System.FormatException)
				{
					// a key that wasn't a number
				}

			} while (!cont);
		}

		static void DeleteAllUnsupportedOperatingSystems()
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

		static void ModifyOperatingSystem(int id)
		{
			OperatingSys os = GetOperatingSystemById(id);
			Console.Clear();
			char operation = '0';
			bool cont = false;
			ConsoleKeyInfo cki;
			WriteHeader("Update Operating System");
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
								Console.WriteLine("Updated Operating System Nane: ");
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
					cont = ValidateYorN(k);
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
	}
}
