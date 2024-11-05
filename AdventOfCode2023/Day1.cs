using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day1
    {
        private string[] input;
        private Dictionary<string, string> wordsToNumber = new Dictionary<string, string>
            {
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6" },
                { "seven", "7" },
                { "eight", "8" },
                { "nine", "9" }
            };

        public Day1(string[] input) {
            this.input = input;
        }

        // Substitutes word number for string number left to right
        private string SubstituteWordForNumberLeftRight(int i)
        {
            string line = "";
            for (int j = 0; j != input[i].Length; j++)
            {
                line += input[i][j].ToString();
                foreach (var word in wordsToNumber)
                {
                    line = line.Replace(word.Key, word.Value);
                }
            }
            return line;
        }

        // Substitutes word number for string number right to left
        private string SubstituteWordForNumberRightLeft(int i)
        {
            string line = "";
            for (int j = input[i].Length-1; j != -1; j--)
            {
                line = input[i][j].ToString() + line;
                foreach (var word in wordsToNumber)
                {
                    line = line.Replace(word.Key, word.Value);
                }
            }
            return line;
        }

        // Day 1: Trebuchet?!
        public int Trebuchet()
        {
            int trebuchetTotal = 0;
            string lineNumber = "0";
            string line = "";
            byte lineValue;
            string valueToConvert = "0";

            for (int i = 0; i != input.Length; i++)
            {
                // Get left to right in proper format
                line = SubstituteWordForNumberLeftRight(i);
                for (int j = 0; j != line.Length; j++)
                {
                    // Part 2: Substitute words for numbers
                    valueToConvert = line[j].ToString();
                    bool isNumberType = byte.TryParse(valueToConvert, out lineValue);
                    if (isNumberType)
                    {
                        lineNumber = line[j].ToString();
                        break;
                    }
                }

                // Get right to left in proper format
                line = SubstituteWordForNumberRightLeft(i);
                for (int j = line.Length - 1; j != -1; j--)
                {
                    valueToConvert = line[j].ToString();
                    bool isNumberType = byte.TryParse(valueToConvert, out lineValue);
                    if (isNumberType)
                    {
                        lineNumber += line[j].ToString();
                        break;
                    }
                }

                // Add to total and reset
                trebuchetTotal += int.Parse(lineNumber);
                lineNumber = "";
            }
            Console.WriteLine(trebuchetTotal);
            return trebuchetTotal;
        }
    }
}
