using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class GlobalSnowProduction
    {
        private string[] input;
        public GlobalSnowProduction(string[] input) {
            this.input = input;
        }

        // Day 1: Trebuchet?!
        public int Trebuchet()
        {
            int trebuchetTotal = 0;
            string lineNumber = "";
            byte lineValue;
            string valueToConvert = "0";

            for (int i = 0; i != input.Length; i++)
            {
                // Get left to right number
                for (int j = 0; j != input[i].Length; j++)
                {
                    valueToConvert = input[i][j].ToString();
                    bool isNumberType = byte.TryParse(valueToConvert, out lineValue);
                    if (isNumberType)
                    {
                        lineNumber = input[i][j].ToString();
                        break;
                    }
                }

                // Case: No number found in line so will not find backwards
                if (lineNumber == "")
                {
                    break;
                }

                // Get right to left number
                for (int j = input[i].Length-1; j != -1; j--)
                {
                    valueToConvert = input[i][j].ToString();
                    bool isNumberType = byte.TryParse(valueToConvert, out lineValue);
                    if (isNumberType)
                    {
                        lineNumber += input[i][j].ToString();
                        break;
                    }
                }
                Console.WriteLine(lineNumber);
                // Add to total and reset
                trebuchetTotal += int.Parse(lineNumber);
                lineNumber = "";


            }
            Console.WriteLine(trebuchetTotal);
            return trebuchetTotal;
        }
    }
}
