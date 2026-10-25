#nullable enable

public class Scheduler {
	private List<Core> _cores;
	private List<Process> _processes;

	public Scheduler(List<Core> cores, List<Process> processes) {
		_cores = cores;
		_processes = processes;
	}

	// Returns true if workload is finished processing
	public bool Process(double delta) {
		bool processing = false;
		foreach (Core core in _cores) {
			if (!core.HasProcess()) {
				// Choose is assumed to modify process list accordingly
				Process? next = Choose();
				if (next is not null) {
					core.Assign(next);
					processing = true;
				}
			} else {
				processing = true;
			}
		}
		return !processing;
	}

	private Process? Choose() {
		// More complex behavior later, for now simply remove
		// and return first item in list
		if (_processes.Count > 0) {
			Process chosen = _processes[0];
			_processes.RemoveAt(0);
			return chosen;
		} else {
			return null;
		}
	}
	
}
