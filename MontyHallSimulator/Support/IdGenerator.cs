using System.Collections.Generic;

namespace MontyHallSimulator.Support
{
    public static class IdGenerator
    {
        private static readonly ICollection<int> GeneratedIds = new List<int>();

        public static int GetId()
        {
            var id = GeneratedIds.Count + 1;
            GeneratedIds.Add(id);
            return id;
        }
    }
}