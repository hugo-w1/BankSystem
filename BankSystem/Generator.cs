using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
	/// <summary>
	/// This class is used to generate specific card and bank account details.
	/// Every function in this class returns strings.
	/// </summary>
	class Generator
	{
		private static Random rng = new Random();
		
		/// <summary>
		/// Generates unique account id with the length of the parameter
		/// </summary>
		/// <param name="length">lkength of the id</param>
		/// <param name="account">The other accounts to see if it makes a duplicate id</param>
		/// <returns>Returns the id in string format</returns>
		public static string AccountID(int length, List<BankAccount> account)
		{
			string chars = "qwertyuiopasdfghjklzxcvbnm1234567890";
			string tmpID = "";

			while (true)
			{
				int passed = 0;
				tmpID = "";
				for (int j = 0; j < length; j++)
				{
					tmpID += chars[rng.Next(0, chars.Length)];
				}
				if (account.Count == 0)
				{
					return tmpID;
				}

				for (int i = 0; i < account.Count; i++)
				{
					if (account[i].AccountID != tmpID)
					{
						passed++;
					}
				}
				if (passed == account.Count)
				{
					return tmpID;
				}
			}

		}


		/// <summary>
		/// Generates a card number split in to four sections with four digits each
		/// </summary>
		/// <returns>Returns the card number</returns>
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

		/// <summary>
		/// Generates a digit code with the length of the int parameter.
		/// This funciton is used for both ccv and pincode.
		/// </summary>
		/// <param name="length">The length of the code</param>
		/// <returns>Returns the code in string format.</returns>
		public static string PinCode(int length)
		{
			string tmpCode = "";
			for (int i = 0; i < length; i++)
			{
				tmpCode += rng.Next(0, 9).ToString();
			}
			return tmpCode;
		}

	}

}
