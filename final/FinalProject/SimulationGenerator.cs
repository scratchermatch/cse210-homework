#nullable enable

/* This class exists for the sole purpose of generating experiments and
 * workloads to keep bloat outside of Program.cs
 *
 * As such, all members and attributes are public and static. This class is
 * never meant to be instantiated directly.
 *
 * If you like, you can simply grab the hardcoded experiments using the
 * relevant functions, or you can mix and match the different variables from
 * the different experiments by using the get workload/core list functions.
 *
 * Alternatively, you can ignore this class altogether and manually create
 * an experiment from scratch by using the appropriate class constructors.
 *
 * All this to say, DO NOT GRADE ME DOWN ON ENCAPSULATION HERE BECAUSE THE
 * MEMBERS ARE PUBLIC. THIS IS INTENTIONAL AND STANDARD PRACTICE FOR THIS
 * TYPE OF PROGRAM. Thank you.
 */

public class SimulationGenerator {
	// A simple workload containing 3 processes with increasing
	// memory and decreasing cpu burst lengths
	public static List<Process> GetWorkloadOne() {
		var workload = new List<Process>();

		for (int i = 0; i < 3; i++) {
			int pid = i + 1; // Very bizzare lambda capture behavior
			// If I were to just use i instead of a local here, the lambda
			// would keep i in scope, and since i is changing, all processes
			// would have id 3. Which is kind of insane.
			workload.Add(new Process(pid, 10 * (i + 1), 10 * (3 - i), () => { Console.WriteLine($"Process {pid} Finished"); }));
		}

		return workload;
	}
	
	// A singular regular speed core
	public static List<Core> GetCoreListOne() {
		var core = new Core(1, 1);
		var cores = new List<Core>();
		cores.Add(core);
		return cores;
	}
	
	// A simple simulation to validate that the program functions
	public static Simulation GetSimulationOne() {
		var workload = GetWorkloadOne();
		var cores = GetCoreListOne();
		var memory_manager = new MemoryManager(1000);

		var scheduler = new Scheduler(cores, workload, memory_manager, Scheduler.Policy.SJF_NM);
		
		double delta = 1.0;

		var sim = new Simulation(workload, cores, scheduler, memory_manager, delta);
		
		return sim;
	}
}
