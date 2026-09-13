using System;

class Program
{
    static void Main(string[] args) {
		DisplayWelcome();
		string name = PromptUserName();
		int number = PromptUserNumber();
		int birth_year;
		PromptUserBirthYear(out birth_year);
		DisplayResult(name, SquareNumber(number), birth_year);
    }
	
	// Displays welcome message
	static void DisplayWelcome(){
		Console.WriteLine("Welcome to the Program!");
	}
	
	// Asks user for a name and returns it as a string
	static string PromptUserName(){
		Console.Write("What is your name? ");
		return Console.ReadLine();
	}
	
	// Asks user for a number and returns it as an int
	static int PromptUserNumber(){
		int number;

		while (true){
			Console.Write("Enter a number: ");
			if (! int.TryParse(Console.ReadLine(), out number)) {
				Console.WriteLine("Try again but with an integer this time");
				continue;
			} else {
				return number;
			}
		}
	}
	
	// Asks user for a year and sets target to response
	static void PromptUserBirthYear(out int target){
		Console.Write("What is your birth year? ");
		target = PromptUserNumber();
	}
	
	// Returns the given integer times itself
	static int SquareNumber(int num){
		return num * num;
	}
	
	// Prints out a given name, squared number, and how many years old you are.
	static void DisplayResult(string name, int square, int year){
		int current_year = DateTime.Now.Year;
		Console.WriteLine($"{name}, your squared number is {square} and you are turning {current_year - year} years old this year.");
	}

}
