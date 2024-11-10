using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day3
    {
        /*
         Strategy is to find symbols, then find any adjacent numbers to add to total
         */
        private string[] input;
        private string validSymbols = "!@#$%^&*()_+-=/";
        private int total;
        private string numberToCheck;

        public Day3(string[] input)
        {
            this.input = input;
        }

        // On any given point in the schematic, returns the number along the x axis.
        private int GetNumberOnPosition(int pointerX, int pointerY)
        {
            string number = this.input[pointerY][pointerX].ToString();

            // Check right
            for (int i = pointerX + 1; i != input[pointerY].Length; i++)
            {
                numberToCheck = input[pointerY][i].ToString();
                bool isNumber = int.TryParse(numberToCheck, out _);
                if (isNumber)
                {
                    number += numberToCheck;
                }
                else
                {
                    break;
                }
            }

            // Check left
            for (int j = pointerX - 1; j >= 0; j--)
            {
                numberToCheck = input[pointerY][j].ToString();
                bool isNumber = int.TryParse(numberToCheck, out _);
                if (isNumber)
                {
                    number = numberToCheck + number;
                }
                else
                {
                    break;
                }
            }
            // Assemble number
            return int.Parse(number);
        }

        private void CheckPosition(int x, int y)
        {
            numberToCheck = input[y][x].ToString();
            bool isNumber = int.TryParse(numberToCheck, out _);
            if (isNumber)
            {
                int number = GetNumberOnPosition(x, y);
                total += number;
            }
        }

        // Day 3: Gonodla lift
        public void GondolaLift()
        {
            
            for (int y = 0; y < input.Length; y++)
            {
                string line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char pointer = input[y][x];
                    bool isSymbol = this.validSymbols.Contains(pointer);
                    if (isSymbol)
                    {
                        bool isNumber;
                        // Check Top
                        numberToCheck = input[y-1][x].ToString();
                        isNumber = int.TryParse(numberToCheck, out _);
                        if (isNumber)
                        {
                            CheckPosition(x, y-1);
                        }
                        else
                        {
                            // Check Top-Left
                            CheckPosition(x-1, y-1);
                            // Check Top-Right
                            CheckPosition(x+1, y-1);
                        }
                        
                        // Check Left
                        CheckPosition(x-1, y);
                        // Check Right
                        CheckPosition(x+1, y);

                        // Check Bottom
                        numberToCheck = input[y+1][x].ToString();
                        isNumber = int.TryParse(numberToCheck, out _);
                        if (isNumber)
                        {
                            CheckPosition(x, y+1);
                        }
                        else
                        {
                            // Check Bottom-Left
                            CheckPosition(x-1, y+1);
                            // Check Bottom-Right
                            CheckPosition(x+1, y+1);
                        }
                    }
                }
            }
            Console.WriteLine($"Day 3: {total}");
        }
    }
}
