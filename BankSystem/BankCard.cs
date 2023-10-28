using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
	/// <summary>
	/// Holds all of the bank card properties.
	/// Everything in this class is readonly as all of the data is generated and does not need to change.
	/// </summary>
	abstract class BankCard
	{
		private readonly string cardNumber = Generator.CardNumber();
		private readonly string ccv = Generator.PinCode(3);
		private readonly string pinCode = Generator.PinCode(4);
		private readonly string expirartionDate = DateTime.Now.Month.ToString() + "/" + DateTime.Now.AddYears(4).Year.ToString();

		/// <summary>
		/// The Objects Card Number
		/// </summary>
		public string CardNumber
		{
			get { return cardNumber; }
		}
		/// <summary>
		/// The Ccv number
		/// </summary>
		public string Ccv
		{
			get { return ccv; }
		}
		/// <summary>
		/// The Pin Code
		/// </summary>
		public string PinCode
		{
			get { return pinCode; }
		}
		/// <summary>
		/// The cards expiration date [MM/YYYY]
		/// </summary>
		public string ExpirationDate
		{
			get { return expirartionDate; }
		}

		public virtual void Transaction(BankAccount sender, BankAccount reciver, double amount)
		{
			Console.WriteLine("Transaction not avaible");
			//Transaction method override in BankAccount Class.
		}
	}
}
