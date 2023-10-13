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
}
