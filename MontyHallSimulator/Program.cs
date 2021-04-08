using System;
using static MontyHallSimulator.Support.Randomizer;

namespace MontyHallSimulator
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Welcome to the Monty Hall Problem Simulator!");
            Console.WriteLine();

            var simCount = SelectSimCount();
            Console.WriteLine();
            var simulator = SetupSimulations(simCount);

            Console.WriteLine($"Setup complete, running {simCount} simulations:");
            foreach (var (sim, result) in simulator.Run())
            {
                Console.WriteLine($"Simulation with Door {sim.ChosenDoorNr} and Choice {sim.ChangeDoor} resulted in {result}");
            }
        }

        private static int SelectSimCount()
        {
            while (true)
            {
                Console.WriteLine("Please select the number of simulations you would like to run:");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var simCount)) return simCount;

                Console.WriteLine($"'{input}' is not a valid number of simulations, please try again with a full Integer value");
            }
        }

        private static Simulator SetupSimulations(int simCount)
        {
            var sim = new Simulator();

            Console.WriteLine($"Please configure the {simCount} simulations by entering the following values:");
            Console.WriteLine("Note that you can select 'R' and the simulator will randomize a choice for you");
            Console.WriteLine();

            for (var i = 1; i <= simCount; i++)
            {
                Console.WriteLine($"Setup simulation {i}/{simCount}");
                var doorNr = DoorInput();
                var choice = DoorChange();
                sim.AddSimulation(new Simulation(doorNr, choice));
                Console.WriteLine();
            }

            return sim;
        }

        private static int DoorInput()
        {
            while (true)
            {
                Console.WriteLine("Door (1-3/R):");
                var input = Console.ReadLine();
                if (input == "R")
                {
                    return RandomDoor();
                }
                if (int.TryParse(input, out var doorNumber)) return doorNumber;

                Console.WriteLine($"'{input}' is not a valid input for which door to select");
            }
        }

        private static bool DoorChange()
        {
            while (true)
            {
                Console.WriteLine("Change Door (Y/N/R):");
                var input = Console.ReadLine();
                if (input == "R")
                {
                    return RandomChoice();
                }
                switch (input)
                {
                    case "N":
                        return false;
                    case "Y":
                        return true;
                }

                Console.WriteLine($"'{input}' is not a valid input for which door to select, please try again with either 'Y' for Yes, 'N' for No, or 'R' for Random");
            }
        }
    }
}
