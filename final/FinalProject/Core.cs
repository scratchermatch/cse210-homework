#nullable enable

public class Core {
	private int _id;
	private int _speed; // consider this a multiplier to added completion per delta
	private Process? _current_process = null;

	public Core(int id, int speed) {
		_id = id;
		_speed = speed;
		if (_speed < 1) {
			_speed = 1;
		}
	}

	public void Assign(Process process) {
		_current_process = process;
	}

	public bool HasProcess() {
		return _current_process != null;
	}

	public bool? Process(double delta) {
		bool? finished = _current_process?.Execute(delta * _speed);
		if (finished == true) {
			_current_process = null;
			return true;
		}
		return false;
	}
}
