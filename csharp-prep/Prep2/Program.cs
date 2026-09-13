using System;

class Program {
    static void Main(string[] args) {
		
		int grade_int;
	    
		while (true) {
			Console.Write("whats your percentage? ");
			if (int.TryParse(Console.ReadLine(), out grade_int)) {
				break;
			} else {
				Console.WriteLine("You're so funny (I only accept integers)");
			}
		}

		string grade_str;

		switch (grade_int){
			case < 60:
				grade_str = "F";
				break;
			case < 70:
				grade_str = "D";
				break;
			case < 80:
				grade_str = "C";
				break;
			case < 90:
				grade_str = "B";
				break;
			case >= 90:
				grade_str = "A";
				break;
			default:
				Console.WriteLine("Something very very bad happened");
				grade_str = "?";
		}
		
		bool pass = grade_int >= 70 ? true : false;
		string pass_message = "good job";
		string fail_message = "better luck next time";

		Console.WriteLine($"You got a {grade_str} which means you {(pass ? "pass" : "fail")}.");
		Console.WriteLine(pass ? pass_message : fail_message);
	}
}
