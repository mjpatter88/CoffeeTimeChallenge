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
            System.Console.WriteLine("Challenge 1:");
            System.Console.WriteLine("Find three digits X, Y and Z such that XYZ in base10 (ten) is equal to ZYX in base9 (nine)? ");
            challengeOne();
            System.Console.WriteLine("\nChallenge 2:");
            System.Console.WriteLine("Write 1,000,000 as the product of two numbers; neither of which contains any zeroes");
            challengeTwo();
            System.Console.WriteLine("\nChallenge 3:");
            System.Console.WriteLine("Use the digits 0-9 to create two numbers. What is the highest product you can achieve when these two numbers are multiplied together?");
            challengeThree();
            System.Console.WriteLine("\nChallenge 4:");
            System.Console.WriteLine("Arrange the numerals 1-9 into a single fraction that equals exactly 1/3 (one third).");
            System.Console.WriteLine("No other math symbols wanted; just concatenation some digits for the numerator, and some to make a denominator.");
            challengeFour();
            System.Console.WriteLine("\nChallenge 5:");
            System.Console.WriteLine("I roll three dice, and multiply the three numbers together. What is the probability the total will be odd?");
            challengeFive();
            System.Console.WriteLine("\nChallenge 6:");
            System.Console.WriteLine("I open up a Word document and type all the numbers 1-10000, separated by spaces, (I did not use any 'thousands' punctuation; just raw numbers).");
            System.Console.WriteLine("Then, my daughter came along and used search and replace, and changed all the digits '0' into spaces.");
            System.Console.WriteLine("If I now sum up all the numbers in the document what is the total? (Any number delineated by one or more spaces is a distinct number)");
            challengeSix();
            System.Console.WriteLine("\nChallenge 7:");
            System.Console.WriteLine("In a room there are a mixture of people and dogs. There are 72 heads, and 200 legs. How many dogs are in the room?");
            System.Console.WriteLine("(No tricks, no chromosomal abnormalities, no disabilities …)");
            challengeSeven();


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

        /// <summary>
        /// Arrange the numerals 1-9 into a single fraction that equals exactly 1/3 (one third).
        /// No other math symbols wanted; just concatenation some digits for the numerator, and some to make a denominator.
        /// 
        /// Loop through possible nominators, calculate the corresponding denominator, then check if it meets the criteria.
        /// </summary>
        private static void challengeFour()
        {
            for (int i = 1; i < int.MaxValue; i++)
            {
                int denom = i * 3;
                //Skip cases with 0's in them.
                if(i.ToString().Contains('0') || denom.ToString().Contains('0'))
                {
                    continue;
                }
                int length = i.ToString().Length + denom.ToString().Length;
                if (length == 9)
                {
                    int[] arr = new int[9];
                    foreach (char c in i.ToString()+denom.ToString())
                    {
                        arr[int.Parse(c.ToString())-1]++;   //should never have a '0' character
                    }
                    bool answer = true;
                    foreach (var item in arr)
                    {
                        if (item != 1)
                        {
                            answer = false;
                            break;
                        }
                    }
                    if (answer)
                    {
                        System.Console.WriteLine(string.Format("Numerator: {0}. Denominator: {1}", i, denom));
                    }
                }
                else if (length > 9)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// I roll three dice, and multiply the three numbers together. What is the probability the total will be odd?
        /// 
        /// I am doing this programmatically, but I could do it in my head easily.
        /// In order to get an odd product, all three dice have to be odd.
        /// Probability of any one die being odd is 1/2.
        /// So answer = 1/2 * 1/2 * 1/2 = 1/8 = 0.125%
        /// </summary>
        private static void challengeFive()
        {
            int odd = 0;
            int total = 0;
            //basic brute force method
            for (int i = 1; i < 7; i++)
            {
                for (int j = 1; j < 7; j++)
                {
                    for (int k = 1; k < 7; k++)
                    {
                        int product = i * j * k;
                        if ((product % 2) != 0)
                        {
                            odd++;
                        }
                        total++;
                    }
                }
            }
            System.Console.WriteLine(string.Format("{0} odds in {1} attempts. {2}% odd.", odd, total, (double)odd/total));
        }

        /// <summary>
        /// I open up a Word document and type all the numbers 1-10000, separated by spaces, (I did not use any 'thousands' punctuation; just raw numbers). 
        /// Then, my daughter came along and used search and replace, and changed all the digits '0' into spaces. 
        /// If I now sum up all the numbers in the document what is the total? 
        /// (Any number delineated by one or more spaces is a distinct number).
        /// </summary>
        private static void challengeSix()
        {
            int[] arr = new int[10000];
            for (int i = 0; i < 10000; i++)
            {
                if((i+1).ToString().Contains('0'))
                {
                    char[] delimeters = new char[] { '0' }; //I do this so I can use the "remove empty entries" option.
                    string[] nums = (i + 1).ToString().Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string num in nums)
                    {
                        arr[i] += int.Parse(num);
                    }
                }
                else
                {
                    arr[i] = i+1;   //index starts at 0 but the values start at 1
                }
            }
            System.Console.WriteLine(arr.Sum());
        }

        /// <summary>
        /// In a room there are a mixture of people and dogs. There are 72 heads, and 200 legs. How many dogs are in the room? 
        /// (No tricks, no chromosomal abnormalities, no disabilities …)
        /// 
        /// Again, brute force seems easy enough.
        /// </summary>
        private static void challengeSeven()
        {
            for (int people = 0; people < 73; people++)
            {
                int dogs = 72 - people;
                int legs = 2 * people + 4 * dogs;
                if (legs == 200)
                {
                    System.Console.WriteLine(string.Format("{0} people and {1} dogs.", people, dogs));
                }
            }
        }
    }
}
