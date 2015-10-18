using System;

namespace big_numbers
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			LargeNumber n1 = new LargeNumber (-5, 0);
			LargeNumber n2 = new LargeNumber (8, -6);
			Console.WriteLine (n1);
			Console.WriteLine (n1 == n2);
		}
	}
}
