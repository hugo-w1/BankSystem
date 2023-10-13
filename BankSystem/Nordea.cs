using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
	class Nordea : BankAccount
	{
		public Nordea(string initalHolder, double initalBalance)
		{
			AccountHolder = initalHolder;
			Balance = initalBalance;
			TransactionFee = 0.045;
			BankName = "Nordea";
		}
	}
}
