﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Controller
    {
        private string[] input;
        // Controller that runs 
        public Controller() {
            LoadData();
        }

        // Loads day data into class
        private void LoadData()
        {
            try
            {
                string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
                string path = $"{projectPath}\\AdventOfCode2023\\Input\\Day 4\\day4-1.txt";
                this.input = File.ReadAllLines(path);
            }
            catch {
                Console.WriteLine("There was an issue getting data from Input folder");
            }
        }

        public void Run() { 
            // Load data
            Day4_2 g = new Day4_2(input);
            g.ScratchCards();
        
        }
    }
}
