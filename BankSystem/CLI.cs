using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
	class CLI
	{

		public static List<BankAccount> Accounts = new List<BankAccount>();

		/// <summary>
		/// Load the accounts from the json file.
		/// </summary>
		public static void LoadAccounts()
		{
			FileInfo file = new FileInfo("./data.json");
			//Check if the data file is empty.
			//If it is empty, do not load it.
			if (file.Length != 0)
			{
				Accounts = DataSaver.ReadFromJsonFile<List<BankAccount>>("./data.json");

			}
		}

		/// <summary>
		/// Shows the start meny and handels the options.
		/// </summary>
		public static void StartMeny()
		{
			LoadAccounts();

			Console.Clear();
			Console.WriteLine("1) New Account");
			Console.WriteLine("2) View Accounts");
			Console.WriteLine("3) New Transaction");
			Console.WriteLine("4) View Transacton History");

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
						case 3:
							Transaction();
							break;
						case 4:
							ViewHistory();
							break;
						default:
							Console.WriteLine("Chose from 1-4");
							break;
					}
					if (option > 0 && option < 4)
					{
						break;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}

		public static BankAccount FindAccount(string id)
		{
			BankAccount tmpAccount = null;

			for (int i = 0; i <= Accounts.Count -1; i++)
			{
				if (id == Accounts[i].AccountID)
				{
					tmpAccount = Accounts[i];
					Console.WriteLine($"Found Account {Accounts[i].AccountHolder} [{Accounts[i].AccountID}]");
					return tmpAccount;
				}
			}
			return null;
		}

		public static void Transaction()
		{

			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("-Transaction-");
			if (Accounts.Count < 2)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("You need more than one account to make transactions");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("\nClick Return To Return Home");

				Console.ReadKey();
				StartMeny();
			}

			string tmpSender = "";
			string tmpReciver = "";
			BankAccount sender = null;
			BankAccount reciver = null;


				while (true)
				{
					Console.Write("Sender ID: ");
					tmpSender = Console.ReadLine();
					sender = FindAccount(tmpSender);
					if (sender == null)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"Could not find account [{tmpSender}]");
						Console.ForegroundColor = ConsoleColor.Blue;
					}
					else
					{
						break;
					}
				}


			while (true)
			{

				Console.Write("Reciver ID: ");
				tmpReciver = Console.ReadLine();
				if (tmpReciver == tmpSender)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Cant send to the same account");
					Console.ForegroundColor = ConsoleColor.Blue;
				}
				reciver = FindAccount(tmpReciver);
				if (reciver == null)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Could not find account [{tmpReciver}]");
					Console.ForegroundColor = ConsoleColor.Blue;
				}
				else
				{
					break;
				}


			}




			double tmpAmount = 0;
			while (true)
			{
				try
				{
					Console.Write("Amount: ");
					tmpAmount = double.Parse(Console.ReadLine());
					if (tmpAmount > sender.Balance)
					{
						throw new Exception("Insufficient funds");
					}
					else
					{
						sender.Balance -= tmpAmount;
						reciver.Balance += tmpAmount;

						sender.TransactionHistory.Add($"[{DateTime.Now}] - Sent {tmpAmount} to {reciver.AccountHolder} [{reciver.AccountID}]");
						reciver.TransactionHistory.Add($"[{DateTime.Now}] - Recived {tmpAmount} from {sender.AccountHolder} [{sender.AccountID}]");


						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine($"{sender.AccountHolder} sent ${tmpAmount} to {reciver.AccountHolder}");
						Console.ForegroundColor = ConsoleColor.White;

						//Save to the json file.
						DataSaver.WriteToJsonFile<List<BankAccount>>("./data.json", Accounts, false);

						break;
					}
				}
				catch (Exception e)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(e.Message);
					Console.ForegroundColor = ConsoleColor.Blue;

				}
			}

			Console.WriteLine("Click return to return home");
			Console.ReadLine();
			StartMeny();

		}

		public static void CreateAccount()
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("-Account Creation-");
			Console.ForegroundColor = ConsoleColor.White;
			string tmpAccountHolder = "";
			double tmpBalance = 0;
			while (true)
			{
				Console.Write("Account Holder: ");
				tmpAccountHolder = Console.ReadLine();
				if (tmpAccountHolder.Trim() != "")
				{
					break;
				}
			}
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
						throw new Exception("Chose between 1-3");
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
				case 2:
					Accounts.Add(new SEB(tmpAccountHolder, tmpBalance));
					break;
				case 3:
					Accounts.Add(new Nordea(tmpAccountHolder, tmpBalance));
					break;
			}

			//Assign an ID to the account.
			Accounts[Accounts.Count - 1].AccountID = Generator.AccountID(4);

			//Save the new account to the json file.
			DataSaver.WriteToJsonFile<List<BankAccount>>("./data.json", Accounts, false);

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Account Created {Accounts[Accounts.Count - 1].AccountHolder}");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\nClick Return To Return Home");
			Console.ReadLine();
			//Go back to start meny
			StartMeny();

		}

		public static void ViewHistory()
		{
			BankAccount account = null;
			if (Accounts.Count < 1)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("You need at least one account to see");
				Console.ForegroundColor = ConsoleColor.White;
			}
			else
			{
				while (true)
				{
					Console.Write("Account Id: ");
					string tmpId = Console.ReadLine();
					account = FindAccount(tmpId);
					if (account == null)
					{
						Console.WriteLine($"Could not find account {tmpId}");
					}
					else
					{
						break;
					}
				}
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine($"- {account.AccountHolder}s Transaction History -");

				for (int i = 0; i < account.TransactionHistory.Count; i++)
				{
					//Make Odd entries White bg and black fg for better Readability.
					if (i % 2 == 0)
					{
						Console.BackgroundColor = ConsoleColor.White;
						Console.ForegroundColor = ConsoleColor.Black;
					}
					Console.WriteLine($"{i + 1} {account.TransactionHistory[i]}");
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
				}
				Console.WriteLine("\nClick Return To Return Home");
				Console.ReadLine();
				StartMeny();
			}

		}

		public static void ViewAccounts(List<BankAccount> Accounts)
		{
			Console.Clear();

			for (int i = 0; i < Accounts.Count; i++)
			{
				//Make Odd entries White bg and black fg for better Readability.
				if (i % 2 == 0)
				{
					Console.BackgroundColor = ConsoleColor.White;
					Console.ForegroundColor = ConsoleColor.Black;
				}
				Console.WriteLine($"{i + 1}: {Accounts[i].AccountHolder}  [{Accounts[i].AccountID}] Balance: ${Accounts[i].Balance}");

				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
			}
			Console.WriteLine("\nClick Return To Return Home");
			Console.ReadLine();

			StartMeny();
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
}
