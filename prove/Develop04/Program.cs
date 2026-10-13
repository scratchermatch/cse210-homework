/* Mindfulness Program - Alexander Hearn
 * All code is dedicated to the public domain under CC0
 *
 * Quite basic program, could be significantly cleaner if not subjected to the manifold
 * shackles of OOP. I was able to clean up most of the bloat by throwing together a clean
 * template inside of StartActivity in Activity.cs which allows all activities to be
 * called with empty constructors, and only need to override one abstract function.
 *
 * Creativity is shown by avoiding repeats in reflection activity as previously mentioned,
 * and handling various menu entries as shown below. Not much else stands out except for the
 * particularly clean spinner code in Activity.cs and the aforementioned template inside
 * StartActivity.
 *
 * Program is generally stable, could probably crash it with some integer overflows in the
 * wait time, or "softlock" it by entering a very large integer to wait in seconds.
 */

using System;

// Look how pretty and clean this is.
class Program {
    static void Main(string[] args) {
		while (true) {
			ShowMenu();
			string input = Console.ReadLine();
			switch (input.ToLowerInvariant()) {
				case "1":
				case "b":
				case "breathing":
				case "breathing activity":
					// Insanely, different case statements are
					// in the same scope, creating name conflicts
					var b_activity = new BreathingActivity();
					b_activity.StartActivity();
					break;
				case "2":
				case "r":
				case "reflection":
				case "reflection activity":
					var r_activity = new ReflectionActivity();
					r_activity.StartActivity();
					break;
				case "3":
				case "l":
				case "listing":
				case "listing activity":
					var l_activity = new ListingActivity();
					l_activity.StartActivity();
					break;
				case "4":
				case "q":
				case "quit":
				case "exit":
					Console.WriteLine("Goodbye!");
					Environment.Exit(0); // Success
					break;
				default:
					Console.WriteLine("Command not recognized. Try again. For example, type '2' or 'r' for reflection activity.");
					break;
			}
		}
    }

	static private void ShowMenu() {
		Console.WriteLine("""
		Menu Options:
		1, (B)reathing Activity
		2, (R)eflection Activity
		3, (L)isting Activity
		4, (Q)uit
		Type an option an press enter
		""");
	}
}
