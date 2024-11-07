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
        private int powerTotal = 0;
        private int gameID = 0;
        private bool isValidGame = true;
        private string[] gameRoundItems;
        private string[] input;
        private Dictionary<string, int> roundResultsAllowed = new Dictionary<string, int>
        {
            { "blue", 14 },
            { "red", 12 },
            { "green", 13 }
        };
        private Dictionary<string, int> roundResults = new Dictionary<string, int>
        {
            { "blue", 0 },
            { "red", 0 },
            { "green", 0 },
        };
        private Dictionary<string, int> gameMinimums = new Dictionary<string, int>
        {
            { "blue", 1 },
            { "red", 1 },
            { "green", 1 },
        };

        public Day2(string[] input)
        {
            this.input = input;
        }

        // Resets game into a starting state
        private void ResetRoundResults()
        {
            this.roundResults["blue"] = 0;
            this.roundResults["red"] = 0;
            this.roundResults["green"] = 0;
        }

        // Resets game minimum cubes needed into a starting state
        private void ResetMinimumCubesInGame()
        {
            this.gameMinimums["blue"] = 1;
            this.gameMinimums["red"] = 1;
            this.gameMinimums["green"] = 1;
        }

        // Adds game outcome values based on a given round in preparation for checking if it was a valid game
        private void LoadRoundResults(string line)
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

        // Loads initial round cubes into dictionary for further processing
        private void LoadRoundCubes(string roundValues)
        {
            string[] cubeSet = roundValues.Split(",");
            string roundValue;
            for (int i = 0; i < cubeSet.Length; i++)
            {
                if (cubeSet[i].Contains("blue"))
                {
                    roundValue = cubeSet[i].Replace(" blue", "");
                    this.roundResults["blue"] += int.Parse(roundValue);
                }
                else if (cubeSet[i].Contains("red"))
                {
                    roundValue = cubeSet[i].Replace(" red", "");
                    this.roundResults["red"] += int.Parse(roundValue);
                }
                else if (cubeSet[i].Contains("green"))
                {
                    roundValue = cubeSet[i].Replace(" green", "");
                    this.roundResults["green"] += int.Parse(roundValue);
                }
            }
        }

        // Updates the minimum required cubes needed to play for Part 2 solution
        private void UpdateMinimumCubesInGame()
        {
            if (this.roundResults["blue"] > this.gameMinimums["blue"])
            {
                this.gameMinimums["blue"] = this.roundResults["blue"];
            }
            if (this.roundResults["red"] > this.gameMinimums["red"])
            {
                this.gameMinimums["red"] = this.roundResults["red"];
            }
            if (this.roundResults["green"] > this.gameMinimums["green"])
            {
                this.gameMinimums["green"] = this.roundResults["green"];
            }
        }

        // Cross references current loaded data values with valid game requirements
        private bool ValidateGameOutcome()
        {
            if (this.roundResults["blue"] > this.roundResultsAllowed["blue"])
            {
                return false;
            }
            else if (this.roundResults["red"] > this.roundResultsAllowed["red"])
            {
                return false;
            }
            else if (this.roundResults["green"] > this.roundResultsAllowed["green"])
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
                isValidGame = true;
                LoadRoundResults(input[i]);
                for (int j=0; j != gameRoundItems.Length; j++)
                {
                    string roundItems = gameRoundItems[j];
                    ResetRoundResults();
                    LoadRoundCubes(roundItems);
                    UpdateMinimumCubesInGame();

                    // Part 2 - regardless of valid/invalid game, add to power total
                    if (j == gameRoundItems.Length - 1)
                    {
                        int gameTotal = this.gameMinimums["blue"] * this.gameMinimums["red"] * this.gameMinimums["green"];
                        this.powerTotal += gameTotal;
                        ResetMinimumCubesInGame();
                    }

                    isValidRound = ValidateGameOutcome();
                    if (isValidRound == false)
                    {
                        isValidGame = false;
                    }
                    ResetRoundResults();

                    // At the end of the game, add to total if all rounds true
                    if (isValidGame && j == gameRoundItems.Length-1)
                    {
                        total += gameID;
                    }
                }
            }
            Console.WriteLine($"Part 1 total: {this.total}");
            Console.WriteLine($"Part 2 total: {this.powerTotal}");
        }
    }
}
