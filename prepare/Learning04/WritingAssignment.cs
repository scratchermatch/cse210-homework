public class WritingAssignment : Assignment {
	private string _title;

	public WritingAssignment(string studentName, string topic, string title) : base(studentName, topic) {
		_title = title;
	}

	public string GetWritingInformation() { // Should be virtual in parent
		return _title + " by " + _studentName;
	}
}
