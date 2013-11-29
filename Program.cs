using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp_UniverseCurrencyConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            ConverterOperations operations = new ConverterOperations();
            
            InputParser parser = new InputParser();
            Dictionary<string, float> string_NumberStore = parser.ReadFileAndParseByLine(@"C:\ThoughtWorks\InputData1.txt");

            if (string_NumberStore != null)
            {
                Dictionary<string, string> storeConvertedCurrency = parser.SolveQuery(@"C:\ThoughtWorks\InputData1.txt", string_NumberStore);
                //parser.OutputData(storeConvertedCurrency);
            }
            Console.ReadKey();

        }
    }
}
