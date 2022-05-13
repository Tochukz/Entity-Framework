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
						//DisplayOperatingSystems();
					}
					else if (result == 9)
					{
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
				Console.WriteLine("\r\nPlease select from the list belowfor what you would like to do today.");
				Console.WriteLine("1. Add a New Machine");
				Console.WriteLine("2. Add a New Operating System");
				Console.WriteLine("3. Add a New Warranty Provider");
				Console.WriteLine("9. Exit Menu");
				cki = Console.ReadKey();
				try
				{

				}
				catch(System.FormatException)
				{
					// a key that wasn't a number 
				}
			} while (!cont);
		}

		static void WriteHeader(string headerText)
		{
			Console.WriteLine(string.Format("{0," + ((Console.WindowWidth / 2) + headerText.Length / 2) + "}", headerText));
		}

		static bool ValidateYorN(string entry)
		{
			bool result  = false;
			if (entry.ToLower() == "y" || entry.ToLower() == "n")
			{
				return true;
			}
			return result;
		}
	}
}
