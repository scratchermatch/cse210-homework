#nullable enable

using System;

class Program {
    static void Main(string[] args) {
		Simulation exp1 = SimulationGenerator.GetSimulationOne();
		exp1.Start();
	}
}
