public class SingleGoal : Goal {
	public SingleGoal(string label, string desc, int points, bool complete = false) : base(label, desc, points, complete) {}

	public override bool Mark(ref int score) {
		if (_complete) {
			Console.WriteLine("Goal is already complete!");
			return true;
		}
		score += _points;
		_complete = true;
		return true;
	}

	public override string Serialize() {
		return $"single|{_label}|{_desc}|{_points}|{_complete}~";
	}
}
