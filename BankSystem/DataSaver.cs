using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankSystem
{


	//Some Code Snippets are taken from this blog:
	//https://blog.danskingdom.com/saving-and-loading-a-c-objects-data-to-an-xml-json-or-binary-file/


	/// <summary>
	/// A class to handel all of the data entering and leaving the program.
	/// </summary>
	public static class DataSaver
	{


		/// <summary>
		/// Writes a list to a file.
		/// </summary>
		/// <param name="filePath">The file path that holds the json file</param>
		/// <param name="objectToWrite">The object to write</param>
		/// <param name="append">If it is true the object to write is appended to the file. Else the file is eresed and then the object is wirtten to the file.</param>
		public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
		{
			TextWriter writer = null;
			try
			{
				string contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
				writer = new StreamWriter(filePath, append);
				writer.Write(contentsToWriteToFile);
			}
			finally
			{
				if (writer != null)
					writer.Close();
			}
		}

		/// <summary>
		/// Reads a json file to and returns it as a list
		/// </summary>
		/// <param name="filePath">The file path to read from.</param>
		/// <returns></returns>
		public static T ReadFromJsonFile<T>(string filePath) where T : new()
		{
			TextReader reader = null;
			try
			{
				reader = new StreamReader(filePath);
				var fileContents = reader.ReadToEnd();
				return JsonConvert.DeserializeObject<T>(fileContents);
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}
	}
}
