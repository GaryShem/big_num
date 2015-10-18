using System;

namespace big_numbers
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			LargeNumber n1 = new LargeNumber (10, 3);
		    Console.WriteLine(n1);
			Console.WriteLine (n1.GetDecimalString());

		    Console.ReadKey();
		}
	}
}
