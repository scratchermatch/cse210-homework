public class InfGoal : Goal {
	public InfGoal(string label, string desc, int points, bool complete = false) : base(label, desc, points, complete) {}

	public override bool Mark(ref int score) {
		score += _points;
		return false;
	}

	public override string Serialize() {
		return $"inf|{_label}|{_desc}|{_points}|{_complete}~";
	}
}
