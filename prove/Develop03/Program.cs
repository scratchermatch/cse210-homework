/* Scripture Memorizer - Alexander Hearn
 * License: CC0 - Public Domain
 *
 * Hardest part was dealing with the disgusting mess that is C# Dictionaries.
 * Second hardest part was learning C# string splicing, joining, etc.
 *
 * I have a word to say about the reference class:
 * It literally exists for the purpose of ferrying information around.
 * WHY are we forced to make its members private? Furthermore, the class's
 * utility is greatly dubious. My Bible class could return a Scripture from
 * 4 arguments, but instead I write 4 getters that just return the value.
 *
 * Scripture could store everything reference stores as its own private
 * attributes. Bible only need to see them once and never again. Nobody else
 * needs access except scripture, and I prefer writing 4 private attributes
 * than 30 lines of boilerplate and an extra file.
 */

using System;

class Program {
    static void Main(string[] args) {
        Bible bible = new Bible("kjv.txt");
		Scripture scripture;

		while (true) { // reference select loop
			Console.WriteLine("\nWhich Bible verses would you like to memorize?");
			string input = Console.ReadLine();

			Reference reference = TryParseReference(input);

			if (reference != null) {
				scripture = new Scripture(reference, bible);
				break;
			}
		}

		while (true) { // quickest dirtiest control flow ever written
			Console.Clear();
			Console.WriteLine(scripture.ToString());
			string input = Console.ReadLine();
			if (input.ToLower() == "quit"){
				System.Environment.Exit(1);
			}

			if (scripture.HideNumWords(5)) { // don't ask questions
				Console.Clear();
				Console.WriteLine(scripture.ToString());
				Console.ReadLine(); // Not even I'm sure how it works
				System.Environment.Exit(1); // (thats not true I said it for dramatic effect)
			}
		}
	}
	
	// It's not pretty but it works
	private static Reference TryParseReference(string input) {
		string book = String.Join(" ", input.Split(" ")[..^1]); // Genesis
		string[] numbers = input.Split(" ")[^1].Split(":"); //[11, 12-13]
		if (!int.TryParse(numbers[0], out int chapter)) { // 11
			Console.WriteLine($"Parser: Invalid chapter: {numbers[0]}");
			return null;
		}
		string[] range;
		if (numbers[1].Contains("-")) {
			range = numbers[1].Split("-");
		} else {
			range = [numbers[1]];
		}
		int start_verse;
		if (!int.TryParse(range[0], out start_verse)){ // 12-13
			Console.WriteLine($"Parser: Invalid verse: {range[1]}");
			return null;
		}
		int end_verse = start_verse;
		if (range.Length > 1) {
			if (!int.TryParse(range[1], out end_verse)){ // 12-13
				Console.WriteLine($"Parser: Invalid verse: {range[1]}");
				return null;
			}
		}

		Reference parsed = new Reference(book, chapter, start_verse, end_verse);

		return parsed;
	}
}
