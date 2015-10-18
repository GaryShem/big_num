using System;
using System.Numerics;
using System.Text;

namespace big_numbers
{
	public class LargeNumber
	{
		private BigInteger _numerator;
		private BigInteger _denominator;
        public int Precision { get; set; }

		public BigInteger Denominator
		{ 
			get { return _denominator; }
			set 
			{ 
				if (value == 0)
					throw new ArgumentException ();
			    _denominator = value;
			    CutFraction ();
			} 
		}

		public BigInteger Numerator{ 
			get{ return _numerator; } 
			set
			{ 
				_numerator = value; 
				CutFraction ();
			}
		}

		public LargeNumber () : this (0, 1)
		{
		}

		public LargeNumber (BigInteger numerator, BigInteger denominator)
		{
			_numerator = numerator;
			Denominator = denominator;
		    Precision = 80;
			CutFraction ();
		}

		public static LargeNumber operator + (LargeNumber n1, LargeNumber n2)
		{
			BigInteger numerator, denominator;

			if (n1.Denominator != n2.Denominator)
			{
				denominator = n1.Denominator * n2.Denominator;
				numerator = n1.Numerator * n2.Denominator + n2.Numerator * n1.Denominator;
			}
			else
			{
				denominator = n1.Denominator;
				numerator = n1.Numerator + n2.Numerator;
			}
			var result = new LargeNumber (numerator, denominator);
			result.CutFraction ();
			return result;
		}

		public static LargeNumber operator - (LargeNumber n1, LargeNumber n2)
		{
			BigInteger numerator, denominator;

			if (n1.Denominator != n2.Denominator)
			{
				denominator = n1.Denominator * n2.Denominator;
				numerator = n1.Numerator * n2.Denominator - n2.Numerator * n1.Denominator;
			}
			else
			{
				denominator = n1.Denominator;
				numerator = n1.Numerator - n2.Numerator;
			}
			var result = new LargeNumber (numerator, denominator);
			result.CutFraction ();
			return result;
		}

		public static LargeNumber operator / (LargeNumber n1, LargeNumber n2)
		{
			BigInteger numerator, denominator;
			numerator = n1.Numerator * n2.Denominator;
			denominator = n2.Numerator * n1.Denominator;
			LargeNumber result = new LargeNumber (numerator, denominator);
			result.CutFraction ();
			return result;
		}

		public static LargeNumber operator * (LargeNumber n1, LargeNumber n2)
		{
			if (n1.Numerator == 0 || n2.Numerator == 0)
			{
				return new LargeNumber (0, 1);
			}
			BigInteger numerator, denominator;
			numerator = n1.Numerator * n2.Numerator;
			denominator = n1.Denominator * n2.Denominator;
			var result = new LargeNumber (numerator, denominator);
			result.CutFraction ();
			return result;
		}
		public void CutFraction()
		{
			BigInteger greatestCommonDivisor = BigInteger.GreatestCommonDivisor (Numerator, Denominator);
			_numerator /= greatestCommonDivisor;
			_denominator /= greatestCommonDivisor;
			if (_denominator < 0)
			{
				_numerator *= -1;
				_denominator *= -1;
			}
		}
		public override string ToString ()
		{
			if (Denominator == 1)
				return Numerator.ToString ();
			return String.Format ("({0}/{1})", Numerator, Denominator);
		}
		public static bool operator ==(LargeNumber n1, LargeNumber n2)
		{
			return n1.Numerator * n2.Denominator == n2.Numerator * n1.Denominator;
		}
		public static bool operator >(LargeNumber n1, LargeNumber n2)
		{
			return n1.Numerator * n2.Denominator > n2.Numerator * n1.Denominator;
		}
		public static bool operator <(LargeNumber n1, LargeNumber n2)
		{
			return n1.Numerator * n2.Denominator < n2.Numerator * n1.Denominator;
		}
		public static bool operator <=(LargeNumber n1, LargeNumber n2)
		{
			return n1.Numerator * n2.Denominator <= n2.Numerator * n1.Denominator;
		}
		public static bool operator >=(LargeNumber n1, LargeNumber n2)
		{
			return n1.Numerator * n2.Denominator >= n2.Numerator * n1.Denominator;
		}
		public static bool operator !=(LargeNumber n1, LargeNumber n2)
		{
			return n1.Numerator * n2.Denominator != n2.Numerator * n1.Denominator;
		}
		public string GetDecimalString()
		{
		    StringBuilder result = new StringBuilder();
            result.Append((Numerator/Denominator).ToString());
		    result.Append(".");
		    BigInteger modus = Numerator % Denominator;
		    int i = 0;
		    bool isComplete = false;
            /////////////////////////////////////////////////////////////
		    while (i < Precision && modus != 0)
		    {
		        if (modus < Denominator)
		        {
		            modus *= 10;
		        }
		        while (modus < Denominator)
		        {
		            modus *= 10;
		            result.Append("0");
		        }
		        result.Append((modus/Denominator).ToString());
		        modus = modus % Denominator;
                i++;
		    }
		    /////////////////////////////////////////////////////////////
		    return result.ToString();
		}
	}
}

