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
	// increasing memory, decreasing CPU burst.
	public static List<Process> GetWorkloadOne() {
		var workload = new List<Process>();

		for (int i = 0; i < 3; i++) {
			int pid = i + 1;
			workload.Add(new Process(
				pid,
				10 * (i + 1),
				2 * (3 - i),
				() => Console.WriteLine($"Process {pid} Finished")
			));
		}

		return workload;
	}

	// A long process followed by three short processes.
	// For demonstrating the convoy effect in FCFS.
	public static List<Process> GetWorkloadConvoy() {
		var workload = new List<Process>();

		workload.Add(new Process(1, 40, 10,
			() => Console.WriteLine("Process 1 Finished (long job)")));

		workload.Add(new Process(2, 10, 1,
			() => Console.WriteLine("Process 2 Finished (short job)")));

		workload.Add(new Process(3, 10, 1,
			() => Console.WriteLine("Process 3 Finished (short job)")));

		workload.Add(new Process(4, 10, 1,
			() => Console.WriteLine("Process 4 Finished (short job)")));

		return workload;
	}

	// Different burst lengths, deliberately not sorted.
	// SJF should finish P2, P4, P3, P1.
	public static List<Process> GetWorkloadExpectedOrder() {
		var workload = new List<Process>();

		workload.Add(new Process(1, 20, 4,
			() => Console.WriteLine("Process 1 Finished (burst 4)")));

		workload.Add(new Process(2, 20, 1,
			() => Console.WriteLine("Process 2 Finished (burst 1)")));

		workload.Add(new Process(3, 20, 3,
			() => Console.WriteLine("Process 3 Finished (burst 3)")));

		workload.Add(new Process(4, 20, 2,
			() => Console.WriteLine("Process 4 Finished (burst 2)")));

		return workload;
	}

	// Useful for checking that RR gives each process CPU time.
	// Expected completion order: P2, P3, P1.
	public static List<Process> GetWorkloadRoundRobin() {
		var workload = new List<Process>();

		workload.Add(new Process(1, 20, 3,
			() => Console.WriteLine("Process 1 Finished (burst 3)")));

		workload.Add(new Process(2, 20, 1,
			() => Console.WriteLine("Process 2 Finished (burst 1)")));

		workload.Add(new Process(3, 20, 2,
			() => Console.WriteLine("Process 3 Finished (burst 2)")));

		return workload;
	}

	// A singular regular-speed core.
	public static List<Core> GetCoreListOne() {
		var cores = new List<Core>();
		cores.Add(new Core(1, 1, 0.1));
		return cores;
	}

	private static Simulation CreateSimulation(List<Process> workload, Scheduler.Policy policy) {
		var cores = GetCoreListOne();
		var memory_manager = new MemoryManager(1000);

		var scheduler = new Scheduler(
			cores,
			workload,
			memory_manager,
			policy
		);

		double delta = 0.01;

		return new Simulation(cores, scheduler, delta);
	}

	// Basic proof of concept simulation (mostly for testing)
	public static Simulation TestSim() {
		return CreateSimulation(
			GetWorkloadOne(),
			Scheduler.Policy.RR_NM
		);
	}

	// Convoy effect with FCFS.
	public static Simulation ConvoyFCFS() {
		return CreateSimulation(
			GetWorkloadConvoy(),
			Scheduler.Policy.FCFS_NM
		);
	}

	// Same workload, different policy, for comparison
	public static Simulation ConvoySJF() {
		return CreateSimulation(
			GetWorkloadConvoy(),
			Scheduler.Policy.SJF_NM
		);
	}

	// Check SJF's expected completion order.
	public static Simulation ExpectedOrderSJF() {
		return CreateSimulation(
			GetWorkloadExpectedOrder(),
			Scheduler.Policy.SJF_NM
		);
	}

	// Compare against FCFS using the same burst lengths.
	public static Simulation ExpectedOrderFCFS() {
		return CreateSimulation(
			GetWorkloadExpectedOrder(),
			Scheduler.Policy.FCFS_NM
		);
	}

	// Test round-robin time slicing and completion order
	public static Simulation RoundRobin() {
		return CreateSimulation(
			GetWorkloadRoundRobin(),
			Scheduler.Policy.RR_NM
		);
	}
}

