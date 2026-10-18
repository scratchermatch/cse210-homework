public abstract class Goal { // This didn't need to exist
	protected bool _complete; // These could all be private
	protected string _label; // if it weren't for inheritance
	protected string _desc;
	protected int _points;

	public Goal(string label, string desc, int points, bool complete = false) { // boilerplate maxxing
		_label = label;
		_points = points;
		_desc = desc;
		_complete = complete;
	}
	
	public abstract bool Mark(ref int score);
	
	// What if I wanted to write almost the same function 3 times?
	public abstract string Serialize();

	public virtual string Display() {
		string status = _complete ? "*" : " ";
		return $"[{status}] {_label} ({_desc})";
	}

	public bool IsComplete() {
		return _complete;
	}

	public int GetPoints() { return _points; }
}
