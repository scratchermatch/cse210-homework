public class MultiGoal : Goal {
	private int _marks;
	private int _bonus;

	public MultiGoal(string label, string desc, int points, int marks, int bonus, bool complete = false) : base(label, desc, points, complete) {
		_marks = marks;
		_bonus = bonus;
	}

	public override bool Mark(ref int score) {
		if (_complete) {
			Console.WriteLine("Goal is already complete!");
			return true;
		}
		score += _points;
		_marks -= 1;
		if (_marks == 0) {
			score += _bonus;
			_complete = true;
			return true;
		}
		return false;
	}

	public override string Serialize() {
		return $"multi|{_label}|{_desc}|{_points}|{_marks}|{_bonus}|{_complete}~";
	}

	public override string Display() {
		return base.Display() + $" -- Remaining: {_marks}";
	}
}
