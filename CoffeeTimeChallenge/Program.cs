using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoffeeTimeChallenge
{
    /// <summary>
    /// An attempt to solve the challenges on this website: http://www.datagenetics.com/blog/june22014/index.html
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Challenge 1: Find three digits X, Y and Z such that XYZ in base10 (ten) is equal to ZYX in base9 (nine)? ");
            challengeOne();

            System.Console.Read();
        }

        /// <summary>
        /// Find three digits X, Y and Z such that XYZ in base10 (ten) is equal to ZYX in base9 (nine)?
        /// </summary>
        private static void challengeOne()
        {
            //Assuming we start at 100 as the smallest 3-digit value
            for (int x = 440; x < 447; x++)
            {
                //int baseNineVal = (x%10) + (((x/10)%10)*(9)) + ((x/100)*(81));
                int baseNineVal = (x % 10) * (81) + (((x / 10) % 10) * (9)) + ((x / 100));
                char a = x.ToString()[0];
                char b = x.ToString()[1];
                char c = x.ToString()[2];
                string baseNineString = c.ToString() + b.ToString() + a.ToString();
                //int a = (x % 10);
                //int b = (((x / 10) % 10) * (9));
                //int c = ((int)(x / 100) * 81);
                //System.Console.WriteLine("a:" + a + " b:" + b + " c:" + c);
                //System.Console.WriteLine(x + ": " + baseNineVal);
                if(baseNineVal == x)
                {
                    System.Console.WriteLine(x + "(in base 10) = " + baseNineString + "(in base 9)");
                }
            }
        }

    }
}
