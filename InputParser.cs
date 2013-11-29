using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp_UniverseCurrencyConverter
{
    class InputParser
    {
        Dictionary<string, string> storeCurrency = new Dictionary<string, string>();      
        Dictionary<string, float> LoadInitialData = new Dictionary<string, float>();
       /// <summary>
       /// Parses the Line & Get the equivalent Number for other currency
       /// </summary>
       /// <param name="fileName"></param>
       /// <returns></returns>
        public Dictionary<string, float> ReadFileAndParseByLine(string fileName)
        {
            string line = string.Empty;
            Dictionary<string, float> string_NumberStore = new Dictionary<string, float>();

            try
            {
                using (StreamReader file = new StreamReader(fileName))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        ParseLine(line);
                    }

                    //Calculate Values in Dictionary
                    foreach (KeyValuePair<string, string> kvp in storeCurrency)
                    {
                        if ((kvp.Key.Contains("(")) && (kvp.Key.Contains(")")))
                        {
                            string[] split = new string[] { "(" };
                            string[] s = kvp.Key.Split(split, StringSplitOptions.RemoveEmptyEntries);
                            int value = ConverterOperations.NumberConstruction(s[0].Replace(" ", string.Empty));
                            if (value == 0)
                            {
                                Console.WriteLine("Conversion Cannot be Done as Succession Criteria Fails ");
                                return null;
                            }
                            else
                            {
                                string_NumberStore.Add(kvp.Key.Replace(s[0], string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Trim(), (float.Parse(kvp.Value) / value));
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured at Function-ReadFileAndParseByLine()",ex.Message);
            }
            return string_NumberStore; 
        }

        public void OutputData(Dictionary<string, string> storeConvertedCurrency)
        {
            foreach (KeyValuePair<string, string> kvp in storeConvertedCurrency)
            {
                if (!kvp.Key.Equals("Error"))
                {
                    Console.WriteLine(kvp.Key + " is " + kvp.Value);
                }
                else
                {
                    Console.WriteLine(kvp.Value);
                }
            }
        }

        /// <summary>
        /// Parse only the lines which are not Queries
        /// </summary>
        /// <param name="line"></param>
        public void ParseLine(string line)
        {           
            string[] split = new string[] { " is " }; 
            string[] s=new string[30];

            try
            {
                if (line.Contains(" is ") && !line.Contains('?'))
                {
                    s = line.Split(split, StringSplitOptions.RemoveEmptyEntries);

                    GetInitialData(s);

                    //Split the first string by space
                    string[] splitBySpace = new string[] { " " };
                    string[] splitValues = s[0].Split(splitBySpace, StringSplitOptions.RemoveEmptyEntries);

                    if (splitValues.Count() > 1)
                    {
                        //Now Handle each currency
                        //Construct a new string to be stored in dictionary
                        bool modified = false;
                        foreach (string value in splitValues)
                        {
                            if (storeCurrency.ContainsKey(value))
                            {
                                s[0] = s[0].Replace(value, storeCurrency[value]);
                            }
                            else
                            {
                                s[0] = s[0].Replace(value, "(" + value + ")");
                            }
                        }
                        if (!(storeCurrency.ContainsKey(s[0].Trim())))
                        {
                            //Extract Numeric Value from string
                            string number = Regex.Match(s[1], @"\d+").Value;
                            storeCurrency.Add(s[0].Trim(), number);
                        }
                    }
                    else
                    {
                        if (!(storeCurrency.ContainsKey(s[0].Trim())))
                        {
                            storeCurrency.Add(s[0].Trim(), s[1]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured at Function-ParseLine()"+ ex.Message);
            }
        }

        private void GetInitialData(string[] s)
        {
            int value = Constants.GetInstance.GetValue(s[1]);

            if (value != 0)
            {
                LoadInitialData.Add(s[0], value);
            }
            else
            {
                string number = Regex.Match(s[1], @"\d+").Value;
                LoadInitialData.Add(s[0].Replace(" ",string.Empty),float.Parse(number));
            }
        }
        /// <summary>
        /// Function which Evaluates the Query After Getting all the values
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="string_NumberStore"></param>
        /// <returns></returns>
        public void SolveQuery(string fileName, Dictionary<string, float> string_NumberStore)
        {
            string line = string.Empty;
            Dictionary<string, string> storeConvertedCurrency = new Dictionary<string, string>();
            try
            {
                using (StreamReader file = new StreamReader(fileName))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        SolveEachQuery(line, string_NumberStore, storeConvertedCurrency);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured at Function" + ex.Message);
            }
            
        }
        /// <summary>
        /// Function which Processes only the lines which are Queries & then returns the calculated Value
        /// </summary>
        /// <param name="line"></param>
        /// <param name="string_NumberStore"></param>
        /// <param name="storeConvertedCurrency"></param>
        public void SolveEachQuery(string line, Dictionary<string, float> string_NumberStore, Dictionary<string, string> storeConvertedCurrency)
        {
            string[] s = new string[30];
            string[] split = new string[] { " is " };

            try
            {

                if (line.Contains(" is ") && line.Contains('?'))
                {
                    s = line.Split(split, StringSplitOptions.RemoveEmptyEntries);
                    string[] splitBySpace = new string[] { " " };
                    string[] splitValues = s[1].Split(splitBySpace, StringSplitOptions.RemoveEmptyEntries);

                    string text = s[1].Replace("?", string.Empty);
                    string storeOriginalText = text.Replace(" ", string.Empty);

                    foreach (string splitVal in splitValues)
                    {
                        if (storeCurrency.ContainsKey(splitVal))
                        {
                            text = text.Replace(splitVal, storeCurrency[splitVal]);
                        }

                        if (string_NumberStore.ContainsKey(splitVal))
                        {
                            text = text.Replace(splitVal, string.Empty);
                        }
                    }

                    float value = ConverterOperations.NumberConstruction(text.Replace(" ", string.Empty));

                    foreach (string splitVal in splitValues)
                    {
                        if (string_NumberStore.ContainsKey(splitVal))
                        {
                            value = value * string_NumberStore[splitVal];
                        }
                    }

                    //Check with LoadInitialData dictionary if the string for
                    //which value to be find is same.
                    float finalValue = 0;
                    foreach (KeyValuePair<string, float> kvp in LoadInitialData)
                    {
                        if (kvp.Key.Equals(storeOriginalText))
                        {
                            finalValue = kvp.Value;
                        }
                    }

                    if (finalValue == 0)
                    {
                        if (s[0].Contains("Credits"))
                        {
                            // storeConvertedCurrency.Add(storeOriginalText, value.ToString() + " Credits");
                            Console.WriteLine(storeOriginalText + " is " + value.ToString() + " Credits");
                        }
                        else
                        {
                            //storeConvertedCurrency.Add(storeOriginalText, value.ToString());
                            Console.WriteLine(storeOriginalText + " is " + value.ToString());
                        }
                    }
                    else
                    {
                        if (s[0].Contains("Credits"))
                        {
                            //storeConvertedCurrency.Add(storeOriginalText, finalValue.ToString() + " Credits");
                            Console.WriteLine(storeOriginalText + " is " + finalValue.ToString() + " Credits");
                        }
                        else
                        {
                            //storeConvertedCurrency.Add(storeOriginalText, finalValue.ToString());
                            Console.WriteLine(storeOriginalText + " is " + finalValue.ToString());
                        }
                    }
                }
                else
                {
                    if (line.Contains('?'))
                    {
                        //storeConvertedCurrency.Add("Error", "I have no idea what you are talking about");
                        Console.WriteLine("I have no idea what you are talking about");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured at function- SolveEachQuery()" + ex.Message);
            }
        }
    }
}
