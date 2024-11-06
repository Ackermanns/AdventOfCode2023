using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day2
    {
        private int total = 0;
        private int gameID = 0;
        private string[] gameRoundItems;
        private string[] input;
        private Dictionary<string, int> maximumCubes = new Dictionary<string, int>
        {
            { "blue", 14 },
            { "red", 12 },
            { "green", 13 }
        };
        private Dictionary<string, int> gameResults = new Dictionary<string, int>
        {
            { "blue", 0 },
            { "red", 0 },
            { "green", 0 },
        };

        public Day2(string[] input)
        {
            this.input = input;
        }

        // Resets game into a starting state
        private void ResetGameResults()
        {
            this.gameResults["blue"] = 0;
            this.gameResults["red"] = 0;
            this.gameResults["green"] = 0;
        }

        // Adds game outcome values based on a given round in preparation for checking if it was a valid game
        private void LoadGameResults(string line)
        {
            try
            {
                string[] fragment = line.Split(':');
                string gameIDStr = fragment[0].Replace("Game ", "");
                gameID = int.Parse(gameIDStr);
                gameRoundItems = fragment[1].Split(";");
            }
            catch
            {
                Console.WriteLine("There was an issue processing the line data, unexpected line format given");
            }
        }

        // Loads round cubes into dictionary for further processing
        private void LoadRoundCubes(string roundValues)
        {
            string[] cubeSet = roundValues.Split(",");
            string roundValue;
            for (int i = 0; i < cubeSet.Length; i++)
            {
                if (cubeSet[i].Contains("blue"))
                {
                    roundValue = cubeSet[i].Replace(" blue", "");
                    this.gameResults["blue"] += int.Parse(roundValue);
                }
                else if (cubeSet[i].Contains("red"))
                {
                    roundValue = cubeSet[i].Replace(" red", "");
                    this.gameResults["red"] += int.Parse(roundValue);
                }
                else if (cubeSet[i].Contains("green"))
                {
                    roundValue = cubeSet[i].Replace(" green", "");
                    this.gameResults["green"] += int.Parse(roundValue);
                }
            }
        }

        // Cross references current loaded data values with valid game requirements
        private bool ValidateGameOutcome()
        {
            if (this.gameResults["blue"] > this.maximumCubes["blue"])
            {
                return false;
            }
            else if (this.gameResults["red"] > this.maximumCubes["red"])
            {
                return false;
            }
            else if (this.gameResults["green"] > this.maximumCubes["green"])
            {
                return false;
            }
            return true;
        }

        // Day 2
        public void CubeConundrum()
        {
            bool isValidRound;
            for (int i = 0; i != input.Length; i++)
            {
                LoadGameResults(input[i]);
                for (int j=0; j != gameRoundItems.Length; j++)
                {
                    string roundItems = gameRoundItems[j];
                    LoadRoundCubes(roundItems);
                    isValidRound = ValidateGameOutcome();
                    if (isValidRound == false)
                    {
                        ResetGameResults();
                        break;
                    }
                    else if (isValidRound ==true && j == gameRoundItems.Length-1)
                    {
                        total += gameID;
                    }
                    ResetGameResults();
                }
            }
            Console.WriteLine(this.total);
        }
    }
}
