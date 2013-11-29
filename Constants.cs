using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ConsoleApp_UniverseCurrencyConverter
{
    class Constants
    {
        private static Constants instance = null;
        private Constants() { }
        public static Constants GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new Constants();
                return instance;
            }
        }
        /// <summary>
        /// Returns the equivalent Integer for a Roman Numeral
        /// </summary>
        /// <param name="romanNumeral"></param>
        /// <returns></returns>
        public int GetValue(string romanNumeral)
        {
            int value=0;

            if (numericConstants.TryGetValue(romanNumeral, out value))
            {
                //Got Value
            }
            else
            {
                //Value Not found
            }

            return value;
        }
        private static readonly Dictionary<string, int> numericConstants = new Dictionary<string, int>() 
                                                                           {  {"I",1},
                                                                              {"V",5},
                                                                              {"X",10},
                                                                              {"L",50},
                                                                              {"C",100},
                                                                              {"D",500},
                                                                              {"M",1000},
                                                                           };

    }
}
