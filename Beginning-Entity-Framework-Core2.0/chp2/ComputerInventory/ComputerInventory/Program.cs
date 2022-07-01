using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ComputerInventory.Models;
using ComputerInventory.Data;
using ComputerInventory.Services;

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
				HelperService.WriteHeader("Welcome to Newbiew Data Systems");
				HelperService.WriteHeader("Main Menu");
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
						OperatingSysService.DisplayOperatingSystems();
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
				HelperService.WriteHeader("Data Entry Menu");
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
						OperatingSysService.AddOperatingSystem();
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

		static void DataModificationMenu()
		{
			ConsoleKeyInfo cki;
			int result = -1;
			bool cont = false;
			do
			{
				Console.Clear();
				HelperService.WriteHeader("Data Modification Menu");
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
						OperatingSysService.SelectOperatingSystem("Delete");
					}
					else if (result == 2)
					{
						OperatingSysService.SelectOperatingSystem("Modify");
					}
					else if (result == 3)
					{
						OperatingSysService.DeleteAllUnsupportedOperatingSystems();
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


	}
}
