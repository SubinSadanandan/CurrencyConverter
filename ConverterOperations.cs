using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ConsoleApp_UniverseCurrencyConverter
{
    class ConverterOperations
    {   
        /// <summary>
        /// Construction of Numeric Numbers Based on the Roman Numeral specified
        /// </summary>
        /// <param name="currency">Roman Numbers</param>
        /// <returns>Returns the integer number if every criteria satisfies</returns>
        public static int NumberConstruction(string currency)
        {
            int length = currency.Length;
            int i = 0, sum = 0; int first, second;

            try
            {
                if (SuccessionCheck(currency))
                {
                    while (i < length)
                    {
                        first = Constants.GetInstance.GetValue(currency[i].ToString());

                        if (i + 1 < length)
                        {
                            second = Constants.GetInstance.GetValue(currency[i + 1].ToString());
                        }
                        else
                            second = 0;

                        if (first >= second)
                        {
                            sum = sum + first;
                            i = i + 1;
                        }
                        else
                        {
                            if (second > first)
                            {
                                //sum = sum + (second - first);
                                //i = i + 2;

                                if (CheckSubstractionLogic(currency[i].ToString(), currency[i + 1].ToString()))
                                {
                                    sum = sum + (second - first);
                                    i = i + 2;
                                }
                                else
                                {
                                    sum = sum + first;
                                    i = i + 1;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured at Function-NumberConstruction():"+ ex.Message);
            }
            return sum;
        }
        /// <summary>
        /// Feeding all successive characters in a dictionary
        /// which shows the count of successive characters if present
        /// </summary>
        /// <param name="currency"></param>
        public static bool SuccessionCheck(string currency)
        {
            Dictionary<char, int> checkSuccessionCount = new Dictionary<char, int>();
            bool status = true;
            try
            {
                for (int i = 0; i < currency.Length; i++)
                {
                    if (checkSuccessionCount.ContainsKey(currency[i]))
                    {
                        if (i > 0)
                        {
                            //Check Adjacent Characters if they are Equal
                            if (currency[i].Equals(currency[i - 1]))
                            {
                                checkSuccessionCount[currency[i]] = checkSuccessionCount[currency[i]] + 1;
                            }
                        }
                    }
                    else
                    {
                        checkSuccessionCount.Add(currency[i], 1);
                    }
                }

                //Loop through all dictionary items to check whether any item violates
                //the count property
                foreach (KeyValuePair<char, int> kvp in checkSuccessionCount)
                {
                    if (kvp.Key.Equals('D') || kvp.Key.Equals('L') || kvp.Key.Equals('V'))
                    {
                        if (kvp.Value > 1)
                            status = false;
                    }

                    if ((kvp.Key.Equals('I')) || (kvp.Key.Equals('X')) || (kvp.Key.Equals('L')) || (kvp.Key.Equals('M')))
                    {
                        if (kvp.Value > 3)
                            status = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occured at Function-SuccessionCheck()" + ex.Message);
            }

            return status;
        }
        /// <summary>
        /// Method to make sure that we are substracting as per conditions Specified
        /// </summary>
        /// <param name="valueToSubstract">Value to be substracted</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckSubstractionLogic(string valueToSubstract, string value)
        {
            bool status = false;

            try
            {
                if ((valueToSubstract.Equals('V')) || (valueToSubstract.Equals('L')) || (valueToSubstract.Equals('D')))
                {
                    status = false;
                }

                int substractedValue = Constants.GetInstance.GetValue(valueToSubstract.ToString());
                int sourceValue = Constants.GetInstance.GetValue(value.ToString());

                if ((sourceValue == (substractedValue * 5)))
                {
                    status = true;
                }

                if ((sourceValue == (substractedValue * 10)))
                {
                    status = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured at Function- CheckSubstractionLogic()",ex.Message);
            }
            return status;
        }
    }
}
