using System;
using System.Collections.Generic;
using System.Linq;
using MontyHallSimulator.Models;

namespace MontyHallSimulator
{
    public record Simulation
    {
        public IList<Door> Doors { get; init; } = SetupDoors();
        public int ChosenDoorNr { get; }
        public bool ChangeDoor { get; }

        public Simulation(int chosenDoorNr, bool changeDoor)
        {
            if (Doors.Count < chosenDoorNr)
                throw new ArgumentException($"Cannot select door {chosenDoorNr} when there are only {Doors.Count} doors");

            ChosenDoorNr = chosenDoorNr;
            ChangeDoor = changeDoor;
        }

        public bool Run()
        {
            var chosenDoor = Doors[ChosenDoorNr-1];

            var doorWithoutCar = Doors.First(x => x.Content == DoorContent.Goat && x.Id != chosenDoor.Id);

            if (ChangeDoor)
                chosenDoor = Doors.First(x => x.Id != doorWithoutCar.Id && x.Id != chosenDoor.Id);

            return chosenDoor.Content == DoorContent.Car;
        }

        private static IList<Door> SetupDoors()
        {
            return new List<Door> { new(DoorContent.Car), new(DoorContent.Goat), new(DoorContent.Goat) }.OrderBy(_ => Guid.NewGuid()).ToList();
        }
    }
}