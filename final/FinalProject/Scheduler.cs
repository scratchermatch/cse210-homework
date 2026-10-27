#nullable enable

public class Scheduler {
	public delegate Process? SchedulingPolicy(out int idx);
	
	public enum Policy {
		FCFS_NM, // First come first served, non memory aware
		SJF_NM, // Shortest job first, non memory aware
		RR_NM, // Round robin, non memory aware
		FCFS, // First come first served
		SJF, // Shortest job first
		RR, // Round robin
		// All of the below are memory aware
		FCFS_MC, // First come first served, optimize for multi core
		SJF_MC, // Shortest job first, optimize for multi core
		RR_MC, // Round robin, optimize for multi core
	}

	private List<Core> _cores;
	private List<Process> _processes;
	private MemoryManager _memory_manager;
	private SchedulingPolicy _policy;

	public Scheduler(List<Core> cores, List<Process> processes, MemoryManager memory_manager, Policy type) {
		_cores = cores;
		_processes = processes;
		_memory_manager = memory_manager;
		switch (type) { // Probably a cleaner way exists, but this works so...
			case Policy.FCFS_NM:
				_policy = FCFS_NM;
				break;
			case Policy.SJF_NM:
				_policy = SJF_NM;
				break;
			default:
				throw new NotImplementedException("Selected scheduler is not yet implemented");
		}
	}

	// Returns true if workload is finished processing
	public bool Process(double delta) {
		bool processing = false;
		foreach (Core core in _cores) {
			if (!core.HasProcess()) {
				Process? next = _policy(out int idx);
				if (next is not null) {
					if (_memory_manager.Alloc(next)) {
						core.Assign(next);
						_processes.RemoveAt(idx);
						processing = true;
					} else {
						// If mem requirements failed, no core can handle
						// this process. Exit early.
						return false;
					}

				}
			} else {
				processing = true;
			}
		}
		return !processing;
	}
	
	// First come first served, do not account for memory
	private Process? FCFS_NM(out int idx) {
		if (_processes.Count > 0) {
			Process chosen = _processes[0];
			idx = 0;
			return chosen;
		} else {
			idx = -1;
			return null;
		}
	}

	// Shortest job first, do not account for memory
	// In a tie, chooses first process.
	private Process? SJF_NM(out int idx) {
		if (_processes.Count > 0) {
			int count = 0;
			double shortest = double.MaxValue;
			idx = -1;
			foreach (Process proc in _processes) {
				if (proc.GetBurst() < shortest) {
					shortest = proc.GetBurst();
					idx = count;
				}
				count ++;
			}
			return _processes[idx];
		} else {
			idx = -1;
			return null;
		}
	}
	
}
