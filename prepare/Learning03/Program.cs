using System;

class Program {
    static void Main(string[] args) {
		Console.WriteLine("Basic Functionality Tests");

		Fraction one = new Fraction();
		Fraction whole = new Fraction(2);
		Fraction rational = new Fraction(2, 3);

		Console.WriteLine($"String: {one.GetFractionString()}, Decimal: {one.GetFractionDecimal()}");
		Console.WriteLine($"String: {whole.GetFractionString()}, Decimal: {whole.GetFractionDecimal()}");
		Console.WriteLine($"String: {rational.GetFractionString()}, Decimal: {rational.GetFractionDecimal()}");
		
		Console.WriteLine("Random Loop Tests (i = 20)");
		
		Random rng = new Random();

		for (int i = 0; i < 20; i++){
			int n = rng.Next();
			int d = rng.Next();
			Fraction r = new Fraction(n, d);

			Console.WriteLine($"String: {r.GetFractionString()}, Decimal: {r.GetFractionDecimal()}");
		}
	}
}
