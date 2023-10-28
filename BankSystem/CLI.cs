using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{

	/// <summary>
	/// The CLI class handels all of the inputs to the program.
	/// </summary>
	class CLI
	{

		private static List<BankAccount> Accounts = new List<BankAccount>();

		/// <summary>
		/// Load the accounts from the json file.
		/// </summary>
		private static void LoadAccounts()
		{
			FileInfo file = new FileInfo("./data.json");
			//Check if the data file is empty.
			//If it is empty, do not load it.
			if (file.Exists == true && file.Length != 0)
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
			Console.ResetColor();
			Console.WriteLine("1) New Account");
			Console.WriteLine("2) View Accounts");
			Console.WriteLine("3) New Transaction");
			Console.WriteLine("4) View Transacton History");
			Console.WriteLine("5) View Account Details");

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
							ViewAccounts(Accounts, true);
							break;
						case 3:
							Transaction();
							break;
						case 4:
							ViewHistory();
							break;
						case 5:
							PrintAccountDetails();
							break;
						default:
							Console.WriteLine("Chose from 1-5");
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


		/// <summary>
		/// Find a bankaccount with a specific id
		/// </summary>
		/// <param name="id">The id of the bankaccount you are looking for</param>
		/// <returns>returns the class object of the bankaccount with the id</returns>
		private static BankAccount FindAccount(string id)
		{
			BankAccount tmpAccount = null;

			for (int i = 0; i <= Accounts.Count - 1; i++)
			{
				if (id == Accounts[i].AccountID)
				{
					tmpAccount = Accounts[i];
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine($"Found Account {Accounts[i].AccountHolder} [{Accounts[i].AccountID}]");
					Console.ResetColor();
					return tmpAccount;
				}
			}
			return null;
		}


		/// <summary>
		/// The trasaction interface asks the user for all of the needed information inorder to make a transaction.
		/// </summary>
		private static void Transaction()
		{

			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("-Transaction- Escape to abort");
			Console.ResetColor();
			if (Accounts.Count < 2)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("You need more than one account to make transactions");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("\nClick Return To Return Home");

				Console.ReadKey();
				StartMeny();
			}

			while (true)
			{
				Console.WriteLine("Do you want to view the available accounts? [y/n]");
			
				string tmpAnswer = ReadLineWithCancel().ToLower().Trim();
				if (tmpAnswer == "y")
				{
					ViewAccounts(Accounts, false);
					break;
				}
				else if(tmpAnswer == "n")
				{
					break;
				}
			}

			string tmpSender = "";
			string tmpReciver = "";
			BankAccount sender = null;
			BankAccount reciver = null;


			while (true)
			{
				Console.Write("Sender ID: ");
				tmpSender = ReadLineWithCancel();
				sender = FindAccount(tmpSender);
				if (sender == null)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Could not find account [{tmpSender}]");
					Console.ResetColor();
				}
				else
				{
					break;
				}
			}


			while (true)
			{

				Console.Write("Reciver ID: ");
				tmpReciver = ReadLineWithCancel();
				if (tmpReciver == tmpSender)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Cant send to the same account");
					Console.ResetColor();
				}
				else
				{
					reciver = FindAccount(tmpReciver);
					if (reciver == null)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"Could not find account [{tmpReciver}]");
						Console.ResetColor();
					}

					else
					{
						break;
					}
				}

			}
			double tmpAmount = 0;
			while (true)
			{
				try
				{
					Console.Write("Amount: ");
					tmpAmount = double.Parse(ReadLineWithCancel());
					if (tmpAmount > sender.Balance)
					{
						throw new Exception("Insufficient funds");
					}
					if (tmpAmount <= 0)
					{
						throw new Exception("Invalid amount");
					}
					else
					{
						//Create a new transaction on the senders behalf.
						sender.Transaction(sender, reciver, tmpAmount);


						//Save to the json file.
						DataSaver.WriteToJsonFile<List<BankAccount>>("./data.json", Accounts, false);

						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine($"{sender.AccountHolder} sent ${tmpAmount} to {reciver.AccountHolder}");
						Console.ForegroundColor = ConsoleColor.White;

						break;
					}
				}
				catch (Exception e)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(e.Message);
					Console.ResetColor();

				}
			}

			Console.WriteLine("Click return to return home");
			Console.ReadLine();
			StartMeny();

		}

		/// <summary>
		/// The CLI interface for account creation.
		/// It asks the user for all the information needed to create a new object from the bankaccount class.
		/// </summary>
		private static void CreateAccount()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("-Account Creation-");
			Console.ForegroundColor = ConsoleColor.White;
			string tmpAccountHolder = "";
			double tmpBalance = 0;
			while (true)
			{
				Console.Write("Account Holder: ");
				tmpAccountHolder = ReadLineWithCancel();
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
					tmpBalance = double.Parse(ReadLineWithCancel());
					if (tmpBalance > 1000000)
					{
						throw new Exception("You cant have over one million");
					}
					break;
				}
				catch (Exception e)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(e.Message);
					Console.ResetColor();
				}
			}
			int tmpBank = 0;
			while (true)
			{
				Console.WriteLine("Chose Bank: ");
				Console.WriteLine("[1 Swedbank] [2 SEB] [3 Nordea]");
				try
				{
					tmpBank = int.Parse(ReadLineWithCancel());
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
			Accounts[Accounts.Count - 1].AccountID = Generator.AccountID(4, Accounts);

			//Save the new account to the json file.
			DataSaver.WriteToJsonFile<List<BankAccount>>("./data.json", Accounts, false);

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Account Created {Accounts[Accounts.Count - 1].AccountHolder} [{Accounts[Accounts.Count - 1].BankName}]");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\nClick Return To Return Home");
			Console.ReadLine();
			//Go back to start meny
			StartMeny();

		}

		/// <summary>
		/// Asks the users for an ID. Then shows the transaction history from the user with that id.
		/// </summary>
		private static void ViewHistory()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("-View History-");
			Console.ResetColor();
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
					Console.WriteLine("Do you want to view the available accounts? [y/n]");

					string tmpAnswer = ReadLineWithCancel().ToLower().Trim();
					if (tmpAnswer == "y")
					{
						ViewAccounts(Accounts, false);
						break;
					}
					else if (tmpAnswer == "n")
					{
						break;
					}
				}

				while (true)
				{
					Console.Write("Account Id: ");
					string tmpId = ReadLineWithCancel();
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
			}

			Console.WriteLine("\nClick Return To Return Home");
			Console.ReadLine();
			StartMeny();

		}

		/// <summary>
		/// Displays all of the accounts in a list.
		/// menyOption is true if the method should go back home direct after.
		/// menyOption is false if it is used from another method that should keep running.
		/// </summary>
		/// <param name="accounts">The accounts to show</param>
		private static void ViewAccounts(List<BankAccount> accounts, bool menyOption)
		{
			if (menyOption)
			{
				Console.Clear();
			}
			if(accounts.Count == 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("No accounts available");
				Console.ResetColor();
			}
			for (int i = 0; i < accounts.Count; i++)
			{
				//Make Odd entries White bg and black fg for better Readability.
				if (i % 2 == 0)
				{
					Console.BackgroundColor = ConsoleColor.White;
					Console.ForegroundColor = ConsoleColor.Black;
				}
				Console.WriteLine($"{i + 1}: {accounts[i].AccountHolder}  [{accounts[i].AccountID}] Balance: ${accounts[i].Balance}");

				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
			}
			if (menyOption)
			{
				Console.WriteLine("\nClick Return To Return Home");
				Console.ReadLine();
				StartMeny();
			}
		}
			

		/// <summary>
		/// Asks the user for an id.
		/// Then prints out all of the details of the specified account.
		/// </summary>
		private static void PrintAccountDetails()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("-Account Details-");
			Console.ResetColor();

			while (true)
			{
				Console.WriteLine("Do you want to view the available accounts? [y/n]");

				string tmpAnswer = ReadLineWithCancel().ToLower().Trim();
				if (tmpAnswer == "y")
				{
					ViewAccounts(Accounts, false);
					break;
				}
				else if (tmpAnswer == "n")
				{
					break;
				}
			}

			BankAccount account = null;
			while (true)
			{
				Console.Write("Account ID: ");
				string accoutid = ReadLineWithCancel();
				account = FindAccount(accoutid);
				if (account == null)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Could not find account [{accoutid}]");
					Console.ForegroundColor = ConsoleColor.Blue;
				}
				else
				{
					break;
				}
			}

			Console.WriteLine($"Holder: \t{account.AccountHolder}");
			Console.WriteLine($"Account ID: \t{account.AccountID}");
			Console.WriteLine($"Tranaction: \t{account.TransactionFee}%");
			Console.WriteLine($"Balance: \t${account.Balance}");
			Console.WriteLine($"Card Number: \t{account.CardNumber}");
			Console.WriteLine($"CCV:      \t{account.Ccv}");
			Console.WriteLine($"Pin Code: \t{account.PinCode}");
			Console.WriteLine($"Exp Date: \t{account.ExpirationDate}");
			Console.WriteLine("__________________________________");

			Console.WriteLine("Click return to return home");
			Console.ReadLine();
			StartMeny();

		}


		/// <summary>
		///String builer that acts like a normal readline but can also listen for specific key presses.
		///If the user click escape it goes back to the start screen.
		/// </summary>
		/// <returns>Returns the input as a string></returns>
		private static string ReadLineWithCancel()
		{
			string result = null;

			StringBuilder buffer = new StringBuilder();

			//The key is read passing true for the intercept argument to prevent
			//any characters from displaying when the Escape key is pressed.
			ConsoleKeyInfo info = Console.ReadKey(true);
			while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
			{
				if (info.Key == ConsoleKey.Backspace && buffer.Length > 0)
				{
					Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
					Console.Write(" ");
					Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
					buffer.Remove(buffer.Length - 1, 1);
					info = Console.ReadKey(true);
				}
				else
				{
					Console.Write(info.KeyChar);
					buffer.Append(info.KeyChar);
					info = Console.ReadKey(true);
				}
			}
			if (info.Key == ConsoleKey.Escape)
			{
				StartMeny();
			}

			if (info.Key == ConsoleKey.Enter)
			{
				result = buffer.ToString();
			}


			Console.WriteLine();
			return result;
		}
	}
}
