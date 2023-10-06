using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{

	class Generator
	{
		static Random rng = new Random();

		public static string AccountID(int length)
		{
			string chars = "qwertyuiopasdfghjklzxcvbnm1234567890";
			string tmpID = "";
			for (int i = 0; i < length; i++)
			{
				tmpID += chars[rng.Next(0, chars.Length)];
			}
			return tmpID;
		}

		public static string CardNumber()
		{
			string tmp = "";
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					tmp += rng.Next(0, 9).ToString();
				}
				tmp += " ";
			}
			return tmp;
		}

		public static string Ccv(int length)
		{
			string tmpCode = "";
			for (int i = 0; i < length; i++)
			{
				tmpCode += rng.Next(0, 9).ToString();
			}
			return tmpCode;
		}

	}

	abstract class BankCard
	{
		private readonly string cardNumber = Generator.CardNumber();
		private readonly string ccv = Generator.Ccv(3);
		private readonly string pinCode = Generator.Ccv(4);
		private readonly string expirartionDate = DateTime.Now.Month.ToString() + "/" + DateTime.Now.AddYears(4).Year.ToString();

		public string CardNumber
		{
			get { return cardNumber; }
		}
		public string Ccv
		{
			get { return ccv; }
		}
		public string PinCode
		{
			get { return pinCode; }
		}
		public string ExpirationDate
		{
			get { return expirartionDate; }
		}
	}

	class BankAccount : BankCard
	{
		private string accountHolder;
		private readonly string accountID = Generator.AccountID(4);
		private string bankName;
		private double balance;
		private double transactionFee;

		public string AccountHolder
		{
			get { return accountHolder; }
			set { accountHolder = value; }
		}
		public string AccountID
		{
			get { return accountID; }
		}
		public string BankName
		{
			get { return bankName; }
			set { bankName = value; }
		}
		public double Balance
		{
			get { return balance; }
			set { balance = value; }
		}
		public double TransactionFee
		{
			get { return transactionFee; }
			set
			{
				if (value <= 0.05)
				{
					transactionFee = value;
				}
				else
				{
					transactionFee = 0.05;
				}
			}
		}

	}

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

	class Program
	{

		static void PrintAccount(BankAccount account)
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

		static void Main(string[] args)
		{

			List<BankAccount> accounts = new List<BankAccount>();

			accounts.Add(new SwedBank("Hugo", 200));
			accounts.Add(new SwedBank("Gutav", 180));

			foreach(BankAccount account in accounts)
			{
				PrintAccount(account);
			}

			Console.ReadLine();
		}
	}
}
