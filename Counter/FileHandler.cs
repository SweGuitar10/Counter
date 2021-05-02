using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counter
{
	static class FileHandler
	{
		// TODO: Give the name of the counter? (Update text box with "counter" or "death")
		public static string Path;
		public static string Text;

		public static void UpdateFile(string str)
		{
			try
			{
				StreamWriter file = new StreamWriter(Path, false);
				file.WriteLine(str);
				file.Close();
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public static int OpenFile()
		{
			OpenFileDialog openDialog = new OpenFileDialog();
			if (openDialog.ShowDialog() == true)
			{
				string file = File.ReadAllText(openDialog.FileName);
				Path = openDialog.FileName;

				int indexOfWs = file.IndexOf(':') + 2;
				int lastIndex = file.IndexOf('\n') - 2;
				string num;
				if (indexOfWs == lastIndex)
				{
					num = $"{file.Substring(indexOfWs)}";
				}
				else
				{
					num = $"{file.Substring(indexOfWs, lastIndex - indexOfWs + 1)}";
				}
				return int.Parse(num);
			}
			return 0;
		}

	}
}
