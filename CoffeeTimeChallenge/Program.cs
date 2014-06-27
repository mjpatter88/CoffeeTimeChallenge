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
            System.Console.WriteLine("\nUse the digits 0-9 to create two numbers. What is the highest product you can achieve when these two numbers are multiplied together?");
            challengeThree();

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
                        System.Console.WriteLine(string.Format("{0}, {1}", x, y));
                    }
                    
                }
            }
        }

        /// <summary>
        /// Use the digits 0-9 to create two numbers. What is the highest product you can achieve when these two numbers are multiplied together?
        /// 
        /// Thoughts: For any givin numbers, we want the highest ones to the left. Basically all we have to figure out is which digits go with
        /// the first number and which go with the second. We could use a 10-digit binary field to test all combinations by incrementing it by one and
        /// using the columns with 1's in them to determine which digits go in the left number.
        /// 9 8 7 6 5 4 3 2 1 0
        /// </summary>
        private static void challengeThree()
        {
            int binaryFlag = 0x001;
            int binaryMax = 0x3FF;      //0b 11 1111 1111

            long maxProduct = 0;
            long maxLeftVal = 0;
            long maxRightVal = 0;

            //We can skip when leftVal would be 0 and when rightVal would be 0;
            while (binaryFlag != binaryMax)
            {
                long leftVal;
                long rightVal;
                calcValues(out leftVal, out rightVal, binaryFlag);

                if (leftVal * rightVal > maxProduct)
                {
                    maxProduct = leftVal * rightVal;
                    maxLeftVal = leftVal;
                    maxRightVal = rightVal;
                }
                binaryFlag++;
            }

            System.Console.WriteLine(string.Format("Product: {0}. Left Value: {1}. Right Value: {2}.", maxProduct, maxLeftVal, maxRightVal));
            //Another answer is to move the 0 in the 1's place to the other number. The product stays the same.
            if (maxLeftVal.ToString()[0] == '0')
            {
                maxLeftVal = int.Parse(maxLeftVal.ToString().Substring(0, maxLeftVal.ToString().Length - 1));   //remove the 0
                maxRightVal = int.Parse(maxRightVal.ToString() + "0");  //add the 0
            }
            else
            {
                maxRightVal = int.Parse(maxRightVal.ToString().Substring(0, maxRightVal.ToString().Length - 1));   //remove the 0
                maxLeftVal = int.Parse(maxLeftVal.ToString() + "0");  //add the 0
            }
            System.Console.WriteLine(string.Format("Product: {0}. Left Value: {1}. Right Value: {2}.", maxProduct, maxLeftVal, maxRightVal));

        }

        private static void calcValues(out long leftVal, out long rightVal, int binaryFlag)
        {
            int[] arr = new int[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            int leftPlace = 0;  //These values track which 10s place to add the current digit to.
            int rightPlace = 0;

            leftVal = 0;
            rightVal = 0;

            //Loop through each position in the bitfield (right to left) (LSB to MSB)
            for (int i = 0; i < 10; i++)
            {

                if(((binaryFlag >> i) & 0x01) == 1)
                {
                    leftVal += (arr[i] * ((long)Math.Pow(10, leftPlace)));
                    leftPlace++;
                }
                else
                {
                    rightVal += (arr[i] * ((long)Math.Pow(10, rightPlace)));
                    rightPlace++;
                }
            }
        }
        
    }
}
