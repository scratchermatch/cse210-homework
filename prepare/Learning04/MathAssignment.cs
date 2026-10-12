// Stop doing abstraction!
public class MathAssignment : Assignment {
	private string _textbookSection; // Should be nullable field in parent class
	private string _problems; // There are many

	public MathAssignment(string studentName, string topic, string textbookSection, string problems) : base(studentName, topic) { // They call me 007:
		_textbookSection = textbookSection; // 0 lines of code saved
		_problems = problems; // 0 tangible benifit
	} // 7 layers of abstraction

	public string GetHomeworkList() { // Rename, belongs in parent class
		return _textbookSection + " " + _problems; // Genuinely these should not be private
	}
}
