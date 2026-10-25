#nullable enable

using System;

class Program {
    static void Main(string[] args) {
		var core = new Core(1, 1);
		var cores = new List<Core>();
		cores.Add(core);
		var workload = WorkloadGenerator.GetWorkloadOne();
		var scheduler = new Scheduler(cores, workload);
		
		double delta = 1.0;

		while (true) {
			bool finished = scheduler.Process(delta);
			foreach (Core c in cores) {
				c.Process(delta);
			}

			if (finished) {
				break;
			}
		}
    }
}
