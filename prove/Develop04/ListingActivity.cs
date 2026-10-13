using System.Diagnostics; // for the stopwatch
// I aint using the DateTime object, that sounds nasty

public class ListingActivity : Activity {
	private static readonly string _description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
	private static readonly string _name = "Listing Activity";
	private static readonly string[] _prompts = [
		"Who are people that you appreciate?",
		"What are personal strengths of yours?",
		"Who are people that you have helped this week?",
		"When have you felt the Holy Ghost this month?",
		"Who are some of your personal heroes?",
	];

	private List<string> _responses = new();

	public ListingActivity() : base(_name, _description){}

	/* Generally unclear from the assignment if the intended flow
	 * is cut user off at exactly duration seconds, or allow them
	 * to finish final response. I chose allow them to finish. */
	protected override void ActivityFlow() {
		Console.WriteLine($"List as many responses as you can to the following prompt:\n{_prompts[_rng.Next(_prompts.Length)]}\nYou may begin in:\n");
		ShowCountdown(5);
		Console.Write("\n");
		
		// Aggressively fancy C# stopwatch
		var timer = Stopwatch.StartNew();
		while (timer.Elapsed.TotalSeconds < _duration){
			_responses.Add(Console.ReadLine());
			// I am aware of the fact that users can enter nothing, and I intentionally allow it
			// Someone that actually cared wouldn't, and I stand to gain nothing from stopping
			// someone from entering nothing.
		}
		timer.Stop(); // Do not go to infinity and beyond
		Console.WriteLine($"You listed {_responses.Count} items");
	}
}
