
namespace FindUriHandlers
{
	using Microsoft.Win32;
	using System;
	using System.Threading.Tasks;
	static class Program
	{
		static void Main(string[] args)
		{
			String[] keynames = Registry.ClassesRoot.GetSubKeyNames();
			Parallel.ForEach<String>(keynames, KeyCheck);
		}



		static void KeyCheck(String keyName)
		{
			RegistryKey key = Registry.ClassesRoot.OpenSubKey(keyName);
			if ((String)key.GetValue("URL Protocol", "-1") == String.Empty)
			{
				String handler = "none";
				RegistryKey subkey = key.OpenSubKey(@"shell\open\command");
				if (subkey != null)
				{
					handler = (string)subkey.GetValue("");
					subkey.Close();
				}
				Console.WriteLine("{0} - {1}", keyName, handler);
			}
			key.Close();
		}
	}
}
