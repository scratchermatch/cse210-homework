#nullable enable

public class Core {
	private int _id;
	private int _speed; // consider this a multiplier to added completion per delta
	private double _swap_time; // Overhead incurred by context switching
	private double _busy_time = 0; // Spend this before sending delta to procs
	private double _time_on_current_proc = 0.0;
	private Process? _current_process = null;
	private bool _finished = true;

	public Core(int id, int speed, double swap_time) {
		_id = id;
		_speed = speed;
		if (_speed < 1) {
			_speed = 1;
		}
		_swap_time = swap_time;
	}

	public void Assign(Process process) {
		_current_process = process;
		_time_on_current_proc = 0.0;
		_finished = false;
	}

	public void Swap(Process process) {
		_busy_time += _swap_time;
		Assign(process);
	}

	public bool IsFinished() { return _finished; }

	public Process? GetCurrentProcess() { return _current_process; }

	public bool? Process(double delta) {
		if (_busy_time >= delta) {
			_busy_time -= delta;
			return false;
		} else if (_busy_time != 0) {
			delta -= _busy_time;
			_busy_time = 0;
		}

		if (_current_process is null) {
			_finished = true;
			return true;
		}
		
		_finished = _current_process.Execute(delta * _speed);
		
		_time_on_current_proc += delta;
		return _finished;
	}

	public double GetCurrentProcTime() { return _time_on_current_proc; }
}
