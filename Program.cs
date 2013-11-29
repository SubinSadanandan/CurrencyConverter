using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ConsoleApp_UniverseCurrencyConverter
{
    class Program
    {
        static void Main(string[] args)
        {

            ConverterOperations operations = new ConverterOperations();         
            InputParser parser = new InputParser();
            Dictionary<string, float> string_NumberStore = parser.ReadFileAndParseByLine(ConfigurationManager.AppSettings["InputFilePath"], operations);

            if (string_NumberStore != null)
            {
                parser.SolveQuery(ConfigurationManager.AppSettings["InputFilePath"], string_NumberStore, operations);         
            }
            Console.ReadKey();

        }
    }
}
