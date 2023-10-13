using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
	class SEB : BankAccount
	{
		public SEB(string initalHolder, double initalBalance)
		{
			AccountHolder = initalHolder;
			Balance = initalBalance;
			TransactionFee = 0.01;
			BankName = "SEB";
		}
	}
}
