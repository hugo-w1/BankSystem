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

	public static class DataSaver
	{

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
