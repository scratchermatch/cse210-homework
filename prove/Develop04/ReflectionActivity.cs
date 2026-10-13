public class ReflectionActivity : Activity {
	private static readonly string _description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";

	private static readonly string _name = "Reflection Activity";

	private static readonly string[] _prompts = [
		"Think of a time when you stood up for someone else.",
		"Think of a time when you did something really difficult.",
		"Think of a time when you helped someone in need.",
		"Think of a time when you did something truly selfless.",
	];

	private static readonly string[] _questions = [
		"Why was this experience meaningful to you?",
		"Have you ever done anything like this before?",
		"How did you get started?",
		"How did you feel when it was complete?",
		"What made this time different than other times when you were not as successful?",
		"What is your favorite thing about this experience?",
		"What could you learn from this experience that applies to other situations?",
		"What did you learn about yourself through this experience?",
		"How can you keep this experience in mind in the future?",
	];

	private const int _q_interval = 5; // short, but what was used in the video

	public ReflectionActivity() : base(_name, _description){}

	protected override void ActivityFlow() {
		int intervals = _duration / _q_interval;
		int remainder = _duration % _q_interval;

		Console.WriteLine(_prompts[_rng.Next(_prompts.Length)]);	
		Console.WriteLine("Press enter when ur ready");
		Console.ReadLine();
		// And then we randomly wait 5 seconds just because we can ig
		Console.Write("Starting in...");
		ShowCountdown(5);
		Console.Write("\n");
		// Avoid duplicate questions, but keep randomized
		for (int i = 0; i < intervals; i++) {
			// Need a reshuffle right before we try to access index 0
			if (i % _questions.Length == 0) {
				// Doesn't require an instance? sure ig
				Random.Shared.Shuffle(_questions);
			}
			Console.WriteLine(_questions[i % _questions.Length]);
			DisplaySpinner(_q_interval);
		}
		if (remainder > 0) {
			Console.WriteLine($"Not enough time for another question! Enjoy the spinner for {remainder} seconds!");
			DisplaySpinner(remainder);
		}
	}
}
