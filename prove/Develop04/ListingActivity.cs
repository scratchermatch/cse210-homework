using System.Diagnostics; // for the stopwatch
// I aint using the DateTime object, that sounds nasty

public class ListingActivity : Activity {
	private static readonly string description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
	private static readonly string name = "Listing Activity";
	private static readonly string[] prompts = [
		"Who are people that you appreciate?",
		"What are personal strengths of yours?",
		"Who are people that you have helped this week?",
		"When have you felt the Holy Ghost this month?",
		"Who are some of your personal heroes?",
	];

	private List<string> responses = new();

	public ListingActivity() : base(name, description){}

	/* Generally unclear from the assignment if the intended flow
	 * is cut user off at exactly duration seconds, or allow them
	 * to finish final response. I chose allow them to finish. */
	protected override void ActivityFlow() {
		Console.WriteLine($"List as many responses as you can to the following prompt:\n{prompts[rng.Next(prompts.Length)]}\nYou may begin in:\n");
		ShowCountdown(5); // There is a strange bug that may be
		// related to my terminal environment that causes the
		// countdown to not backspace the final character. I 
		// couldn't solve it, so I prin a newline to avoid 
		// confusion. This should probably work on Windows, but
		// I have now way to test it.
		Console.Write("\n");
		
		// Aggressively fancy C# stopwatch
		var timer = Stopwatch.StartNew();
		while (timer.Elapsed.TotalSeconds < _duration){
			responses.Add(Console.ReadLine());
		}
		timer.Stop(); // Do not go to infinity and beyond
		Console.WriteLine($"You listed {responses.Count} items");
	}
}
