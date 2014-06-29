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
        private class IntPair
        {
            public int Item1;
            public int Item2;

            public IntPair(int i1, int i2)
            {
                Item1 = i1;
                Item2 = i2;
            }
        }

        static void Main(string[] args)
        {
            bool RUNALL = true;
            if (RUNALL)
            {
                System.Console.WriteLine("Challenge 1:");
                System.Console.WriteLine("Find three digits X, Y and Z such that XYZ in base10 (ten) is equal to ZYX in base9 (nine)? ");
                challengeOne();
            }
            if (RUNALL)
            {
                System.Console.WriteLine("\nChallenge 2:");
                System.Console.WriteLine("Write 1,000,000 as the product of two numbers; neither of which contains any zeroes");
                challengeTwo();
            }
            if (RUNALL)
            {
                System.Console.WriteLine("\nChallenge 3:");
                System.Console.WriteLine("Use the digits 0-9 to create two numbers. What is the highest product you can achieve when these two numbers are multiplied together?");
                challengeThree();
            }
            if (RUNALL)
            {
                System.Console.WriteLine("\nChallenge 4:");
                System.Console.WriteLine("Arrange the numerals 1-9 into a single fraction that equals exactly 1/3 (one third).");
                System.Console.WriteLine("No other math symbols wanted; just concatenation some digits for the numerator, and some to make a denominator.");
                challengeFour();
            }
            if (RUNALL)
            {
                System.Console.WriteLine("\nChallenge 5:");
                System.Console.WriteLine("I roll three dice, and multiply the three numbers together. What is the probability the total will be odd?");
                challengeFive();
            }
            if (RUNALL)
            {
                System.Console.WriteLine("\nChallenge 6:");
                System.Console.WriteLine("I open up a Word document and type all the numbers 1-10000, separated by spaces, (I did not use any 'thousands' punctuation; just raw numbers).");
                System.Console.WriteLine("Then, my daughter came along and used search and replace, and changed all the digits '0' into spaces.");
                System.Console.WriteLine("If I now sum up all the numbers in the document what is the total? (Any number delineated by one or more spaces is a distinct number)");
                challengeSix();
            }
            if (RUNALL)
            {
                System.Console.WriteLine("\nChallenge 7:");
                System.Console.WriteLine("In a room there are a mixture of people and dogs. There are 72 heads, and 200 legs. How many dogs are in the room?");
                System.Console.WriteLine("(No tricks, no chromosomal abnormalities, no disabilities …)");
                challengeSeven();
            }
            if (RUNALL)
            {
                System.Console.WriteLine("\nChallenge 10:");
                System.Console.WriteLine("Put the numbers 1-13 into three buckets with the constraint that the difference between any two pairs of numbers in any bucket is not a number also in that bucket.");
                System.Console.WriteLine("(e.g. If you place 5,7 in a bucket, then you cannot place 2 in that same bucket).");
                challenge10();
            }
            if (RUNALL)
            {
                System.Console.WriteLine("\nChallenge 11:");
                System.Console.WriteLine("I’ve written down all the integers from 1 to 65,502 inclusive.");
                System.Console.WriteLine("I select two of them, cross them out, and multiply them together to get a product.");
                System.Console.WriteLine("When I sum up the remaining 65,500 numbers, I get the same result. What two numbers did I pick?");
                challenge11();
            }
            
            
             

            System.Console.WriteLine("\n\n\nPress any key to exit...");
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
        /// Again, brute force seems easy enough. I could also just use pen and paper with a system of two equations with two unknowns.
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

        /// <summary>
        /// Put the numbers 1-13 into three buckets with the constraint that the difference between any two pairs of numbers in any bucket is not a number also in that bucket. 
        /// (e.g. If you place 5,7 in a bucket, then you cannot place 2 in that same bucket).
        /// 
        /// This problem can be solved in a manner similar to challenge #3. 
        /// Each number must go in one of three places, so I can iterate over all possible options.
        /// </summary>
        private static void challenge10()
        {
            //Each pair represents a number and its bucket assignment
           IntPair[] pairs = new IntPair[13];
            for (int i = 0; i < 13; i++)
			{
                pairs[i] = new IntPair(i+1, 0);
			}

            //We know the final position is with all values at 2.
            //We also know this will not be a solution, so we can end prior to testing it.
            int sum = 0;
            while (sum != 25)
            {
                //check if the current configuration is a solution (complicated process)
                IntPair[] bucket1Nums = Array.FindAll<IntPair>(pairs, element => element.Item2 == 0);
                IntPair[] bucket2Nums = Array.FindAll<IntPair>(pairs, element => element.Item2 == 1);
                IntPair[] bucket3Nums = Array.FindAll<IntPair>(pairs, element => element.Item2 == 2);
                if(checkBucket(bucket1Nums) && checkBucket(bucket2Nums) && checkBucket(bucket3Nums))
                {
                    StringBuilder output = new StringBuilder();
                    output.Append("Bucket1: [");
                    foreach (var item in bucket1Nums)
                    {
                        output.Append(item.Item1 + " ");
                    }
                    output.Append("] Bucket2: [");
                    foreach (var item in bucket2Nums)
                    {
                        output.Append(item.Item1 + " ");
                    }
                    output.Append("] Bucket3: [");
                    foreach (var item in bucket3Nums)
                    {
                        output.Append(item.Item1 + " ");
                    }
                    output.Append("]");
                    System.Console.WriteLine(output.ToString());
                }

                //increment to the next configuration
                for (int i = 0; i < pairs.Length; i++)
                {
                    if (pairs[i].Item2 == 2)
                    {
                        pairs[i].Item2 = 0;
                    }
                    else
                    {
                        pairs[i].Item2++;
                        break;
                    }
                }

                //Update the value of sum
                sum = 0;
                foreach (var pair in pairs)
	            {
                    sum += pair.Item2;
	            }
            }
        }

        private static bool checkBucket(IntPair[] bucketNums)
        {
            for (int i = 0; i < bucketNums.Length-1; i++)
            {
                for (int indexToAdd = 0; indexToAdd < bucketNums.Length; indexToAdd++)
                {
                    for (int cmpToIndex = i + 1; cmpToIndex < bucketNums.Length; cmpToIndex++)
                    {
                        if (bucketNums[i].Item1 + bucketNums[indexToAdd].Item1 == bucketNums[cmpToIndex].Item1)
                        {
                            return false;
                        }
                    }
                }
                
            }

            return true;
        }

        /// <summary>
        /// I’ve written down all the integers from 1 to 65,502 inclusive. 
        /// I select two of them, cross them out, and multiply them together to get a product. 
        /// When I sum up the remaining 65,500 numbers, I get the same result. What two numbers did I pick?
        /// 
        /// Another simple brute force method should work
        /// </summary>
        private static void challenge11()
        {
            int RANGE = 65502;
            int[] arr = new int[RANGE];
            for (int i = 0; i < RANGE; i++)
            {
                arr[i] = i + 1;
            }
            int sum = arr.Sum();    //I can calculate the total sum once, and then just subtract the two numbers. (saves tons of time)
            long count = 0;
            for (int index1 = 0; index1 < RANGE-1; index1++)
            {
                for (int index2 = index1 + 1; index2 < RANGE; index2++)
                {
                    int val1 = arr[index1];
                    int val2 = arr[index2];
                    int newSum = sum - val1 - val2;
                    if (val1 * val2 == newSum)
                    {
                        System.Console.WriteLine(string.Format("Value 1: {0}. Value 2: {1}", val1, val2));
                    }
                    count++;
                    if (count % 500000000 == 0)
                    {
                        System.Console.WriteLine(count/1000000 + " million tested.");
                    }
                }
            }
        }
    }
}
