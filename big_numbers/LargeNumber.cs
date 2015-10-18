using System;
using System.Collections.Generic;
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
		    StringBuilder result = new StringBuilder((Numerator/Denominator).ToString());
            List<Tuple<string, BigInteger>> fractionList = new List<Tuple<string, BigInteger>>();
		    StringBuilder fractionString = new StringBuilder(".");
		    BigInteger modus = Numerator % Denominator;

		    int i = 0;
		    bool isPeriod = false;
            /////////////////////////////////////////////////////////////
		    while (i < Precision && modus != 0 && !isPeriod)
		    {
		        if (modus < Denominator)
		        {
		            modus *= 10;
		        }
		        while (modus < Denominator)
		        {
		            fractionList.Add(new Tuple<string, BigInteger>("0", modus));
		            modus *= 10;
		        }
		        string currentDigit = (modus/Denominator).ToString();
		        BigInteger currentModus = modus;
		        for (int j = 0; j < fractionList.Count; j++)
		        {
		            if (modus == fractionList[j].Item2)
		            {
		                isPeriod = true;
                        Tuple<string, BigInteger> leftParentheses = new Tuple<string, BigInteger>("(", 0);
                        Tuple<string, BigInteger> rightParentheses = new Tuple<string, BigInteger>(")", 0);
                        fractionList.Insert(j, leftParentheses);
		                fractionList.Add(rightParentheses);
		                break;
		            }
		        }
                if (!isPeriod)
		        {
		            fractionList.Add(new Tuple<string, BigInteger>(currentDigit, currentModus));
		            modus = modus%Denominator;
		        }
		        i++;
		    }
		    foreach (Tuple<string, BigInteger> digit in fractionList)
		    {
		        fractionString.Append(digit.Item1);
		    }
		    result.Append(fractionString);
		    /////////////////////////////////////////////////////////////
		    return result.ToString();
		}
	}
}

