public class Simulation {
	private List<Core> _cores;
	private Scheduler _scheduler;
	// Time step passed into participants
	private double _delta_time;
	// Total cumulative time sim has been running
	private double _sim_delta = 0.0;

	public Simulation(List<Core> cores, Scheduler scheduler,
			double delta_time) {
		_cores = cores;
		_scheduler = scheduler;
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

			// Update sim delta last
			// This is because participants calc delta from requested value
			// plus the delta they received
			_sim_delta += _delta_time;

		}
	}

	public double GetSimDelta() { return _sim_delta; }
}
