using System;

class Program
{
    static void Main(string[] args)
    {
		int next_int;
		List<int> numbers = new List<int>();

		while (true) {
			Console.Write("Type an integer: (type 0 to stop) ");
			if (!int.TryParse(Console.ReadLine(), out next_int)) {
				Console.WriteLine("I said to type an integer.");
				continue;
			}
			if (next_int == 0){
				break;
			}

			numbers.Add(next_int);
		}

		int sum = numbers.Sum(); // literally this method exists
		double average = numbers.Average(); // and this method
		int max = numbers.Max(); // and this one
		
		// A quick trip to the documentation saved about 5 minutes
		Console.Write($"\nSum: {sum}\nAverage: {average}\nMax: {max}");
    }
}
