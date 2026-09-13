// How many of these magic number games have I written in how many languages now? 5? 6?

using System;

class Program
{
    static void Main(string[] args)
    {
		Random rng = new Random();
		int magic_number = rng.Next(1, 101); // Inclusive, exclusive
		int guess;

		while (true) {
			Console.Write("Guess a number: ");
			if (! int.TryParse(Console.ReadLine(), out guess)){
				Console.WriteLine("Not an integer brother, try again");
				continue;
			}

			if (guess < magic_number) {
				Console.WriteLine("higher");
			} else if (guess > magic_number) {
				Console.WriteLine("lower");
			} else {
				Console.WriteLine("you got it cool");
				break;
			}
		}
	}
}
