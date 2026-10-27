#nullable enable

public class Process {
	private int _id; // unique identifier for this process
	private int _memory; // memory requirement for this process
	private double _cpu_burst; // anticipated cpu burst length for this process
	// consider cpu burst as max progress
	private double _progress; // consider this as current progress 
	
	public Action _callback; // function to call upon completion

	public Process(int id, int memory, int cpu_burst, Action callback) {
		_id = id;
		_memory = memory;
		_cpu_burst = cpu_burst;
		_callback = callback;
	}
	
	// Returns true if process is finished after delta processing time
	public bool Execute(double delta) {
		_progress += delta;
		if (_progress >= _cpu_burst) {
			_progress = _cpu_burst;
			_callback();
			return true;
		}
		return false;
	}

	public int GetMem() { return _memory; }
	
	public double GetBurst() { return _cpu_burst; }

	public int GetID() { return _id; }
}
