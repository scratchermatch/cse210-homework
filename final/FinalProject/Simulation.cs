public class Simulation {
	private List<Process> _workload;
	private List<Core> _cores;
	private Scheduler _scheduler;
	private MemoryManager _memory_manager;
	private double _delta_time;

	public Simulation(List<Process> workload, List<Core> cores,
			Scheduler scheduler, MemoryManager memory_manager,
			double delta_time) {
		_workload = workload;
		_cores = cores;
		_scheduler = scheduler;
		_memory_manager = memory_manager;
		_delta_time = delta_time;
	}
	
	// Runs the simulation until it is finished
	public void Start() {
		while (true) {
			// Always run scheduler first
			bool finished = _scheduler.Process(_delta_time);
			
			// Exit if finished
			if (finished) {
				break;
			}

			// And then each core
			foreach (Core c in _cores) {
				c.Process(_delta_time);
			}
		}
	}
}
