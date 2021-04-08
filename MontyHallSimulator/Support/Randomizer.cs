using System;

namespace MontyHallSimulator.Support
{
    public static class Randomizer
    {
        public static int RandomDoor() => new Random().Next(1, 3);
        public static bool RandomChoice() => new Random().Next(0, 1) == 1;
    }
}