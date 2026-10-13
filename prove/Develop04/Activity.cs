public abstract class Activity { // Do not instantiate directly
	private static readonly string[] spinner = ["/","-","\\","|"];

	protected int _duration;
	private string _description; // set by constructor of derived class
	private string _name; // same as above

	protected Random rng = new Random();

	public Activity(string name, string description) {
		_description = description;
		_name = name;
	}
	
	// The masterwork tying this mess together
	public void StartActivity() {
		DisplayStartingMessage();
		GetDuration();
		Console.WriteLine("GET READY SCRUB");
		DisplaySpinner(5);
		ActivityFlow();
		DisplayEndingMessage();
	}

	protected abstract void ActivityFlow(); // Must be overridden
	
	// Sets _duration to an int fromt the user
	private void GetDuration() {
		Console.WriteLine("How many seconds would you like the activity to last?");
		while (true) {
			if (int.TryParse(Console.ReadLine(), out _duration)) {
				return;
			} else {
				Console.WriteLine("Not an integer, learn to count and try again :(");
			}
		}
	}

	private void DisplayStartingMessage() {
		Console.WriteLine($"Welcome to the {_name}\n");
		Console.WriteLine(_description + "\n");
	}

	private void DisplayEndingMessage() {
		Console.WriteLine("gg no re\n");
		Console.WriteLine($"you completed {_duration} seconds of the {_name}");
		DisplaySpinner(5);
	}

	// Displays spinner on new line for a given seconds
	protected void DisplaySpinner(int seconds) {
		// Very proud of this function, very clean
		Console.Write("\n");
		for (int i = 0; i < seconds; i++) {
			Console.Write(spinner[i % 4]);
			Thread.Sleep(1000);
			Console.Write("\b");
		}
	}
	
	// Displays countdown with no whitespace for given seconds
	protected void ShowCountdown(int seconds) {
		for (int i = seconds; i > 0; i--) {
			Console.Write($"{i}");
			Thread.Sleep(1000);
			Console.Write("\b");
		}
	}
}
