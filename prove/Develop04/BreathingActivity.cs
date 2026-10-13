public class BreathingActivity : Activity {
	private static readonly string description = "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.";
	private static readonly string name = "Breathing Activity";

	private const int breath_time = 5;

	public BreathingActivity() : base(name, description){}

	protected override void ActivityFlow() {
		int intervals = _duration / breath_time;
		int remainder = _duration % breath_time;
		string[] reminders = ["Breath in...", "Breath out..."];

		for (int i = 0; i < intervals; i++) {
			Console.WriteLine(reminders[i%2]);
			ShowCountdown(breath_time);
		}
		if (remainder > 0){
			Console.WriteLine($"Not enough time for another breath! Enjoy the spinner for {remainder} seconds!");
			DisplaySpinner(remainder);
		}
	}
}
