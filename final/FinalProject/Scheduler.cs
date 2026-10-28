#nullable enable

public class Scheduler {	
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
	private List<Process> _processes; // Will contain ONLY unassigned procs
	private MemoryManager _memory_manager;
	private Action _policy;

	private readonly double _rr_time_quantum = 0.2;

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
			case Policy.RR_NM:
				_policy = RR_NM;
				break;
			default:
				throw new NotImplementedException("Selected scheduler is not yet implemented");
		}
	}

	// Returns true if workload is finished processing
	public bool Process(double delta) {
		_policy();
		
		foreach (Core core in _cores) {
			if (! core.IsFinished()) return false;
		}
		return _processes.Count == 0;
	}
	
	// Free old process a core finished last frame
	// Because I preemptively delete them to simplify this
	// algorithm, I can't just iterate over the queue and check.
	//
	// DOES NOT CHECK THAT THE PROCESS IS FINISHED!
	private void RemoveStaleProcess(Core core) {
		Process? stale = core.GetCurrentProcess();
		if (stale is not null) _memory_manager.FreeProc(stale);
	}
	
	// First come first served, do not account for memory
	private void FCFS_NM() {
		foreach (Core core in _cores) {
			// Early exit if no procs or core busy
			if (! core.IsFinished()) continue;
			if (_processes.Count <= 0) return;

			RemoveStaleProcess(core);
									
			// Assign proc to core
			var next = _processes[0];
			if (! _memory_manager.Alloc(next)) return;
			core.Assign(next);
			_processes.RemoveAt(0);
		}
	}

	// Shortest job first, do not account for memory
	// In a tie, chooses first process.
	private void SJF_NM() {
		foreach (Core core in _cores) {
			// Early exit if no procs or core busy
			if (! core.IsFinished()) continue;
			if (_processes.Count <= 0) return;
			
			RemoveStaleProcess(core);
			
			// Find shortest process
			int count = 0;
			double shortest = double.MaxValue;
			int idx = -1;
			foreach (Process proc in _processes) {
				if (proc.GetBurst() < shortest) {
					shortest = proc.GetBurst();
					idx = count;
				}
				count ++;
			}
			
			// Assign process to core
			var next = _processes[idx];
			if (! _memory_manager.Alloc(next)) return;
			core.Assign(next);
			_processes.RemoveAt(idx);
		}
	}

	
	private void RR_NM() {
		foreach (Core core in _cores) {
		// Assign a process immediately if the core is idle
		// or its previous process has finished.
		if (core.IsFinished()) {
			RemoveStaleProcess(core);
			if (_processes.Count == 0)
				continue;
			Process next = _processes[0];
			if (!_memory_manager.ProcInMem(next) && !_memory_manager.Alloc(next)) {
				throw new Exception("KERNEL PANIC! Cannot allocate enough memory for RR scheduling!");
			}
			_processes.RemoveAt(0);
			core.Assign(next);
			continue;
		}

		// The core is running a process.
		// Don't preempt it until its quantum expires.
		if (core.GetCurrentProcTime() <= _rr_time_quantum)
			continue;

		// No waiting processes; Let the current one continue.
		if (_processes.Count == 0)
			continue;

		// Save the current process at the back of the queue.
		Process? old = core.GetCurrentProcess();

		if (old is not null)
			_processes.Add(old);

		// Take the next process from the front.
		Process nextProcess = _processes[0];

		if (!_memory_manager.ProcInMem(nextProcess) && !_memory_manager.Alloc(nextProcess)) {
			throw new Exception("KERNEL PANIC! Cannot allocate enough memory for RR scheduling!");
		}

		_processes.RemoveAt(0);

		// Swap adds context-switch overhead and assigns the new process.
		core.Swap(nextProcess);
	}
}


}
