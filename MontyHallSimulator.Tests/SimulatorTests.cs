using FluentAssertions;
using MontyHallSimulator.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace MontyHallSimulator.Tests
{
    public class SimulatorTests
    {
        private readonly ITestOutputHelper _helper;

        public SimulatorTests(ITestOutputHelper helper)
        {
            _helper = helper;
        }

        [Theory]
        [InlineData(1, false, true)]
        [InlineData(1, true, false)]
        [InlineData(2, false, false)]
        [InlineData(2, true, true)]
        [InlineData(3, false, false)]
        [InlineData(3, true, true)]
        public void SimulationGetsExpectedResult(int doorNr, bool changeDoor, bool expected)
        {
            var simulator = new Simulator();

            var orderedDoors = new List<Door> { new(DoorContent.Car), new(DoorContent.Goat), new(DoorContent.Goat) };

            simulator.AddSimulation(new Simulation(doorNr, changeDoor) { Doors = orderedDoors });

            var (_, result) = simulator.Run().Single();
            result.Should().Be(expected, $"Door {doorNr} + Choice {changeDoor} should be {expected}");
        }

        [Fact]
        public void SimulatorCanRunMultipleSimulations()
        {
            var timer = Stopwatch.StartNew();

            var simulator = new Simulator();

            var orderedDoors = new List<Door> { new(DoorContent.Car), new(DoorContent.Goat), new(DoorContent.Goat) };

            const int simCount = 1000000;

            var door = 1;
            var choice = true;

            for (var i = 1; i <= simCount; i++)
            {
                simulator.AddSimulation(new Simulation(door, choice) { Doors = orderedDoors });

                if (door == 3)
                    door = 1;
                else
                    door++;
                choice = !choice;
            }

            foreach (var (sim, result) in simulator.Run())
            {
                var expected = ResultByChoices(sim);
                result.Should().Be(expected, $"Door {sim.ChosenDoorNr} + Choice {sim.ChosenDoorNr} should be {expected}");
            }

            timer.Stop();

            _helper.WriteLine($"Ran {simCount} simulations in {timer.ElapsedMilliseconds}ms");

            static bool ResultByChoices(Simulation sim)
            {
                if (sim.ChosenDoorNr == 1 && !sim.ChangeDoor)
                    return true;

                return sim.ChosenDoorNr != 1 && sim.ChangeDoor;
            }
        }
    }
}
