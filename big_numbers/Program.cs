using System;

namespace big_numbers
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			LargeNumber n1 = new LargeNumber (7, 19);
		    Console.WriteLine(n1);
			Console.WriteLine (n1.GetDecimalString());
		}
	}
}
