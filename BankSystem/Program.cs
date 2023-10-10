using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{

	class SwedBank : BankAccount
	{
		public SwedBank(string initalHolder, double initalBalance)
		{
			AccountHolder = initalHolder;
			Balance = initalBalance;
			TransactionFee = 0.05;
			BankName = "SwedBank";
		}
	}


	class CLI
	{

		public static List<BankAccount> Accounts = DataSaver.ReadFromJsonFile<List<BankAccount>>("./data.json");


		/// <summary>
		/// Load the accounts from the json file.
		/// </summary>
		public static void LoadAccounts()
		{
			Accounts = DataSaver.ReadFromJsonFile<List<BankAccount>>("./data.json");
		}
		public static void StartMeny()
		{


			Console.Clear();
			Console.WriteLine("1) Create New Account");
			Console.WriteLine("2) Views Accounts");
			Console.WriteLine("3) View Transacton History");

			while (true)
			{
				try
				{
					int option = int.Parse(Console.ReadLine());

					switch (option)
					{
						case 1:
							CreateAccount();
							break;
						case 2:
							ViewAccounts(Accounts);
							break;
						default:
							Console.WriteLine("Chose from 1-3");
							break;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}

		public static void CreateAccount()
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("-Account Creation-");
			Console.ForegroundColor = ConsoleColor.White;

			Console.Write("Account Holder: ");
			string tmpAccountHolder = Console.ReadLine();
			double tmpBalance = 0;
			while (true)
			{
				try
				{
					Console.Write("Account Balance: ");
					tmpBalance = double.Parse(Console.ReadLine());
					break;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
			int tmpBank = 0;
			while (true)
			{
				Console.WriteLine("Chose Bank: ");
				Console.WriteLine("[1 Swedbank] [2 SEB] [3 Nordea]");
				try
				{
					tmpBank = int.Parse(Console.ReadLine());
					if (tmpBank < 1 || tmpBank > 3)
					{
						throw new Exception("Chose between 1 -3");
					}
					else
					{
						break;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			switch (tmpBank)
			{
				case 1:
					Accounts.Add(new SwedBank(tmpAccountHolder, tmpBalance));
					break;
			}

			//Save the new account to the json file.
			DataSaver.WriteToJsonFile<List<BankAccount>>("./data.json", Accounts);
		}

		public static void ViewAccounts(List<BankAccount> Accounts)
		{
			Console.Clear();

			for (int i = 0; i < Accounts.Count; i++)
			{
				Console.WriteLine($"{i}: {Accounts[i].AccountHolder} ID: {Accounts[i].AccountID} Balance: ${Accounts[i].Balance}");
			}
			Console.WriteLine("\nClick Return To Return Home");
			Console.ReadLine();
			//StartMeny(Accounts);
		}

		public static void PrintAccount(BankAccount account)
		{
			Console.WriteLine($"Holder: \t{account.AccountHolder}");
			Console.WriteLine($"Account ID: \t{account.AccountID}");
			Console.WriteLine($"Tranaction: \t{account.TransactionFee}%");
			Console.WriteLine($"Balance: \t${account.Balance}");
			Console.WriteLine($"Card Number: \t{account.CardNumber}");
			Console.WriteLine($"CCV:      \t{account.Ccv}");
			Console.WriteLine($"Pin Code: \t{account.PinCode}");
			Console.WriteLine($"Exp Date: \t{account.ExpirationDate}");
			Console.WriteLine("__________________________________");


		}
	}


	class Program
	{
		public static List<BankAccount> Accounts = new List<BankAccount>();


		static void Main(string[] args)
		{




			CLI.StartMeny();

			Console.ReadLine();
		}
	}
}
