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
            System.Console.WriteLine("\nWrite 1,000,000 as the product of two numbers; neither of which contains any zeroes");
            challengeTwo();

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
                int baseNineVal = (x % 10) * (81) + (((x / 10) % 10) * (9)) + ((x / 100));
                char a = x.ToString()[0];
                char b = x.ToString()[1];
                char c = x.ToString()[2];
                string baseNineString = c.ToString() + b.ToString() + a.ToString();
                if(baseNineVal == x)
                {
                    System.Console.WriteLine(x + "(in base 10) = " + baseNineString + "(in base 9)");
                }
            }
        }

        /// <summary>
        /// Write 1,000,000 as the product of two numbers; neither of which contains any zeroes.
        /// </summary>
        private static void challengeTwo()
        {
            for (int x = 1; x < 1000; x++)
            {
                if (1000000 % x == 0)   //if x is a factor of 1,000,000
                {
                    int y = 1000000 / x;
                    if((!x.ToString().Contains("0")) && (!y.ToString().Contains("0")))
                    {
                        System.Console.WriteLine(new StringBuilder(string.Format("{0}, {1}", x, y)));
                    }
                    
                }
            }
        }
        
    }
}
