using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day4_1
    {
        private string[] input;
        private List<int> winningNumbers = new List<int> { };
        private List<int> myNumbers = new List<int> { };
        private int total;
        private int cardWinningsTotal;
        private int numCardWinnings;

        public Day4_1(string[] input)
        {
            this.input = input;
        }

        private void LoadCardDetails(string line)
        {
            try
            {
                bool isNumber;
                int numberToAdd;
                string[] items = line.Split(':');
                items = items[1].Split("|");
                string[] winningNumbersToProcess = items[0].Split();
                string[] myNumbersToProcess = items[1].Split();

                for (int i = 0; i < winningNumbersToProcess.Length; i++)
                {
                    isNumber = int.TryParse(winningNumbersToProcess[i], out _);
                    if (isNumber)
                    {
                        numberToAdd = int.Parse(winningNumbersToProcess[i]);
                        winningNumbers.Add(numberToAdd);
                    }
                }
                numberToAdd = -1;

                for (int i = 0; i < myNumbersToProcess.Length; i++)
                {
                    isNumber = int.TryParse(myNumbersToProcess[i], out _);
                    if (isNumber)
                    {
                        numberToAdd = int.Parse(myNumbersToProcess[i]);
                        myNumbers.Add(numberToAdd);
                    }
                }
            }
            catch {
                Console.WriteLine("Issue with processing line, improper format?");
            }
        }

        private void HandleComparison(int winningNumber, int myNumber)
        {
            if (winningNumber == myNumber)
            {
                if (numCardWinnings == 0)
                {
                    cardWinningsTotal = 1;
                }
                else
                {
                    cardWinningsTotal = cardWinningsTotal * 2;
                }
                numCardWinnings += 1;
            }
        }

        private void ResetForNextCard()
        {
            cardWinningsTotal = 0;
            numCardWinnings = 0;
            winningNumbers = new List<int> { };
            myNumbers = new List<int> { };
        }

        public void ScratchCards()
        {
            for (int i = 0; i < input.Length; i++)
            {
                LoadCardDetails(input[i]);
                // For each winning number
                for (int w = 0; w != winningNumbers.Count; w++)
                {
                    // For each of my numbers
                    for (int m = 0; m != myNumbers.Count; m++)
                    {
                        HandleComparison(winningNumbers[w], myNumbers[m]);
                    }
                }
                total += cardWinningsTotal;
                ResetForNextCard();
            }
            Console.WriteLine($"Day 4, Part 1 total: {total}");
        }
    }
}