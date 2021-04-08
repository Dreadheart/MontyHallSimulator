using System.Collections.Generic;
using System.Linq;

namespace MontyHallSimulator
{
    public class Simulator
    {
        public List<Simulation> Simulations { get; } = new();

        public void AddSimulation(Simulation simulation)
        {
            Simulations.Add(simulation);
        }

        public IEnumerable<(Simulation, bool)> Run()
        {
            return Simulations.Select(x => (x, x.Run()));
        }
    }
}