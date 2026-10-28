#nullable enable

using System;

class Program {
    static void Main(string[] args) {
		Console.WriteLine("Test 1: Showing convoy effect. First come first served:");
		var sim1 = SimulationGenerator.ConvoyFCFS();
		sim1.Start();
		Console.WriteLine("Same workload with shortest job first:");
		var sim2 = SimulationGenerator.ConvoySJF();
		sim2.Start();
		Console.WriteLine("Prove that SJF completes jobs in expected order:");
		var sim3 = SimulationGenerator.ExpectedOrderSJF();
		sim3.Start();
		Console.WriteLine("Same workload, FCFS:");
		var sim4 = SimulationGenerator.ExpectedOrderFCFS();
		sim4.Start();
		Console.WriteLine("Look, I made round robin work (not fun) (very hard)");
		var sim5 = SimulationGenerator.RoundRobin();
		sim5.Start();
	}
}
