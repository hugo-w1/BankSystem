using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{

	/// <summary>
	/// Bank account class.
	/// Inherets the bankcard for use in the bank account.
	/// 
	/// </summary>
	class BankAccount : BankCard
	{
		private string accountHolder;
		private readonly string accountID = Generator.AccountID(4);
		private string bankName;
		private double balance;
		private double transactionFee;

		/// <summary>
		/// The name of the account holder
		/// </summary>
		public string AccountHolder
		{
			get { return accountHolder; }
			set { accountHolder = value; }
		}
		/// <summary>
		/// The Id of the account
		/// </summary>
		public string AccountID
		{
			get { return accountID; }
		}

		/// <summary>
		/// Name of the bank, ex Swedbank, SEB
		/// </summary>
		public string BankName
		{
			get { return bankName; }
			set { bankName = value; }
		}
		/// <summary>
		/// Balance, the amount of money in the bankaccount
		/// </summary>
		public double Balance
		{
			get { return balance; }
			set { balance = value; }
		}

		/// <summary>
		/// Tranasaction fee. Should not be more than 5%.
		/// The transacion fee gets decucted from the amomunt sent. Ex if you send 100 with the fee of 5% the reciver recives 95
		/// </summary>
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
}
