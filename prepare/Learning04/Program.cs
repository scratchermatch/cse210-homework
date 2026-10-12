using System;

class Program {
    static void Main(string[] args) {
		var ma = new MathAssignment("bob", "addition", "section 1", "1-10");
		var wa = new WritingAssignment("joe", "history", "how addition was invented");

		Console.WriteLine(ma.GetSummary());
		Console.WriteLine(ma.GetHomeworkList());

		Console.WriteLine(wa.GetSummary());
		Console.WriteLine(wa.GetWritingInformation());
    }
}
