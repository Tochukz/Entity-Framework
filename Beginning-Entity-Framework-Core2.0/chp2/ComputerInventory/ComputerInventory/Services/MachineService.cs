using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ComputerInventory.Models;
using ComputerInventory.Services;
using ComputerInventory.Data;

namespace ComputerInventory.Services
{
	public class MachineService
	{
		public void AddMachine()
		{
			Console.Clear();
			ConsoleKeyInfo cki;
			string result;
			bool cont = false;
			Machine machine = new Machine();
			HelperService.WriteHeader("Add New Machine");
			Console.WriteLine();
			List<MachineType> machineTypes = GetMachineTypes();
			MachineType machineType = new MachineType();
			Models.OperatingSys os = new Models.OperatingSys();
			if (machineTypes.Count == 0)
			{
				machineType = AddMachineType();
			}
			else
			{
				// We have at least one Result so we should display the results forthe user to select from
				DisplayMachineTypes(machineTypes);
				Console.WriteLine("Enter the ID for the Machine Type you would like to use.");
				Console.WriteLine("To add a new Machine Type enter [a]");
				do
				{
					cki = Console.ReadKey(true);
					if (cki.Key == ConsoleKey.A)
					{
						machineType = AddMachineType();
						cont = true;
					}
					else
					{
						if (char.IsNumber(cki.KeyChar))
						{
							int idEntered = Convert.ToInt16(cki.KeyChar.ToString());
							if (machineTypes.Exists(x => x.MachineTypeId == idEntered))
							{
								machineType = machineTypes.Find(x => x.MachineTypeId ==
								idEntered);
								cont = true;
							}
							else
							{
								// No match, could add a counter here and after a certainnumber of attempts
							   // Add in some error handling
							}
						}
					}
				} while (!cont);
			}
			machine.MachineTypeId = machineType.MachineTypeId;
		}

		public List<MachineType> GetMachineTypes()
		{
			List<MachineType> machineTypes = new List<MachineType>();
			using (AppDBContext context = new AppDBContext())
			{
				machineTypes = context.MachineType.ToList();
			}
			return machineTypes;
		}

		public MachineType AddMachineType()
		{
			Console.Clear();
			ConsoleKeyInfo cki;
			string result;
			bool cont = false;
			MachineType mt = new MachineType();
			string mName = "";
			do
			{
				HelperService.WriteHeader("Add New Machine Type");
				Console.WriteLine("Enter a Description for the Machine Type and hit Enter");
				mName = Console.ReadLine();
				if (mName.Length >= 6)
				{
					cont = true;
					mt.Description = mName;
				}
				else
				{
					Console.WriteLine("Please enter a valid Description of at least 6 characters.\r\nPress and key to continue...");
				Console.ReadKey();
				}
			} while (!cont);
			cont = false;

			do
			{
				Console.Clear();
				Console.WriteLine($"You entered {mt.Description} as the Description.\r\nDo you wish to continue? [y or n]");
                cki = Console.ReadKey();
				result = cki.KeyChar.ToString();
				cont = HelperService.ValidateYorN(result);
			} while(!cont);

			if (result.ToLower() == "y")
			{
				bool exists = CheckForExistingMachineType(mt.Description);
				if (exists)
				{
					Console.WriteLine("\r\nMachine Type already exists in the database\r\nPress any key to continue...");
				Console.ReadKey();
				}
				else
				{
					using (AppDBContext context = new AppDBContext())
					{
						Console.WriteLine("\r\nAttempting to save changes...");
						context.MachineType.Add(mt);
						int i = context.SaveChanges();
						if (i == 1)
						{
							Console.WriteLine("Contents Saved\r\nPress any key to continue...");
						    Console.ReadKey();
						}
					}
				}
			}
			return mt;
		}

		public bool CheckForExistingMachineType(string mtDesc)
		{
			bool exists = false;
			using (AppDBContext context = new AppDBContext())
			{
				var mt = context.MachineType.Where(t => t.Description == mtDesc);
				if (mt.Count() > 0)
				{
					exists = true;
				}
			}
			return exists;
		}

		public void DisplayMachineTypes(List<MachineType> machineTypes)
		{
			foreach (MachineType mt in machineTypes)
			{
				Console.WriteLine($"ID: {mt.MachineTypeId} Description: {mt.Description}");
            }
	    }
    }
}
