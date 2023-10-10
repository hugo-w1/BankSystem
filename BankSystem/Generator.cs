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
		static Random rng = new Random();

		/// <summary>
		/// Generates an account id with the length of the parameter
		/// </summary>
		/// <param name="length">lkength of the id</param>
		/// <returns>Returns the id in string format</returns>
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
