/* 
 * Journal Program - Alexander Hearn
 * License: CC0 - Public Domain
 *
 * This was a fairly easy project, the only difficult part was learning
 * where C and C# syntax differed, like only being able to fall through
 * empty break statement or using C# dynamic arrays and strings.
 *
 * Here is how I met all of the program requirements:
 *
 * Functionality:
 * - New: discards current journal
 * - Write: shows random prompt, stores user response, date, and prompt
 * - Display: iterates over all entries and calls their display method
 * - Save: serializes and saves journal in memory to a file
 * - Load: loads and deserializes a saved journal for editing
 * - Quit: exits the program
 *
 * Classes:
 * - Globals: Stores the prompt list so it is not duplicated in every entry
 *   object, also stores generic helper functions and the random generator
 * - Program: Entry point at Main(), core program loop
 * - Journal: Contains list of entries and methods for saving/loading
 * - Entry: Contains user input and metadata for each entry
 *
 * Other Rubric Items: 
 * - I demonstrated abstraction by keeping journal and entry separate;
 *   they do not need to understand how the other works. For example,
 *   Entry provides its own serialize and display functions
 * - I attempt to follow the style guide despite being personally
 *   offended by it; don't judge me too harshly, old habits die hard.
 * 
 * Creativity and Extra Additions:
 * - Multiple command aliases (write, w, 1)
 * - overwrite confirmation
 * - unsaved-change detection
 * - automatic save prompting
 * - custom serialization
 *
 * The program is very stable and should handle the vast majority of malcious
 * interaction from the user, including bad file paths and malformed file data.
 * I think there are probably a few ways to crash it if you were creative
 * enough, but I am comfortable with its current state to submit it.
 */

using System;

// If you needed some extra evidence that C# is not a good language:
// My global variables have to go in a class, and if they don't, then C# automatically
// generates a Program class labeled partial. It doesn't tell you this anywhere. It just
// does it without asking. This is not transparent to the programmer and the compiler
// offers no useful hints as to what is happening. All just to force you into a world
// where every conceivable piece of code is an object in an overly convoluted OOP web.

class Globals {
	public static List<string> _prompts = new List <string> {
		"How many cats did you see today",
		"Why are you the way that you are?",
		"What is the meaning of life, the universe, and everything?",
		"What is the natural logarithm of the integral of 8x^2 sin x from 1 to 2?",
		"Who made you fill out this list?",
		"When in unix time did you execute this program?",
		"How many nuns could a nunchuck chuck if a nunchuck could chuck nuns?",
		"Should I be scared of you?",
		"Do two wrongs make a right?",
		"In this world, is it kill or be killed?",
		"Where is the worst place to try to eat a pizza?",
	};

	public static Random _rng = new Random();

	public static string GetValidFilepath() {
		while (true) {
			Console.WriteLine("Please provide a valid filepath:");
			string path = Console.ReadLine();
			if (IsValidFilepath(path)) {
				return path;
			} else {
				Console.WriteLine("Provided path was not valid, please try again:");
			}
		}
	}
	
	public static bool IsValidFilepath(string path) {
		// not *technically* guaranteed to be a valid path, but at least a string
		// that *could* be interpreted as one. File.Exists should hopefully
		// handle the rest of the edge cases.
		try {
			Path.GetFullPath(path);
			return true;
		} catch (Exception) {
			return false;
		}
	}

	public static bool GetUserConfirmation(string message = null) {
		if (message != null) {
			Console.WriteLine(message);
		}
		string input = Console.ReadLine().ToLowerInvariant();
		if (input == "yes" || input == "y"){
			return true;
		} else {
			return false;
		}
	}
}

class Program { // I will die on this hill. { does not belong on its own line.
    public static Journal _journal = new Journal();

	static void Main(string[] args) {
		string choice;
		while (true) {
			DisplayMainMenu();
			choice = Console.ReadLine().ToLowerInvariant();
			switch (choice) {
				case "new":
				case "n":
				case "0":
					// Create a new journal (let the garbage collector discard the old one)
					_journal.CheckUnsavedData();
					_journal = new Journal();
					break;
				case "write": // So apparently you CAN fall through case statements
				case "w": // but only if they are completely empty
				case "1": // C# is very weird
					// Add an entry to the journal
					_journal.AddEntryRandom();
					break;
				case "display":
				case "d":
				case "2":
					// Display all entries in the current journal
					if (_journal._contents.Count == 0) {
						Console.WriteLine("Current journal contains no entires. Please create or load a journal first.");
					} else {
						_journal.Display();
					}
					break;
				case "load":
				case "l":
				case "3":
					// load a journal from a file
					_journal.CheckUnsavedData();

					Console.WriteLine("Enter the path of the journal file you would like to load:");
					string filepath = Console.ReadLine();
	
					if (!File.Exists(filepath)){
						Console.WriteLine($"Failed to load file from {filepath}: file does not exist");
						continue;
					}

					string serialized_data;
					using (StreamReader reader = new StreamReader(filepath)) {
						serialized_data = reader.ReadLine();
						// Console.WriteLine(serialized_data);
					}
					
					_journal = new Journal();
					_journal.LoadFromSerializedString(serialized_data);
					_journal._filepath = filepath;

					break;
				case "save":
				case "s":
				case "4":
					// save journal to a file
					_journal.Save();
					break;
				case "quit":
				case "q":
				case "5":
					// exit the program
					_journal.CheckUnsavedData();
					System.Environment.Exit(1);
					break;
				default:
					Console.WriteLine($"No command matching {choice} was found");
					break;
			}
		}
    }

	static void DisplayMainMenu() {
		Console.Write("""
				What would you like to do?
				New (n, 0)
				Write (w, 1)
				Display (d, 2)
				Load (l, 3)
				Save (s, 4)
				Quit (q, 5)

				""");
	}
}

class Journal {
	public List<Entry> _contents = new List<Entry>{};
	public string _filepath = null;
	public bool _unsaved_data_exists = false;
	
	public void AddEntryRandom() {
		Entry new_entry = new Entry();
		new_entry.GetFromUser();
		_contents.Add(new_entry);
		_unsaved_data_exists = true;
	}

	public void Save() {
		if (_filepath == null) {
			// i.e. this is a new journal, not one loaded from a file
			Console.WriteLine("Where would you like to save your journal?");
			string new_filepath = Globals.GetValidFilepath();

			if (File.Exists(new_filepath)) {
				// i.e. attempting to overwrite an existing file
				Console.WriteLine("There is already a file at the specified path. Would you like to overwrite it? (y/n)");
				string response = Console.ReadLine().ToLowerInvariant();
				if (response == "y") {
					_filepath = new_filepath;
				} else {
					Console.WriteLine("Operation aborted");
					return;
				}
			} else {
				// valid path and not overwriting a file
				_filepath = new_filepath;
			}
		} else {
			// i.e. this journal was loaded from a file, and therefore should be saved to the same file
			if (!Globals.IsValidFilepath(_filepath)) {
				Console.WriteLine("Error: filepath associated with this journal is invalid, cannot save");
			}
		}
		// Serialize the entire journal and save it to the given path
		using (StreamWriter outputFile = new StreamWriter(_filepath)) {
			outputFile.WriteLine(Serialize());
		}

		_unsaved_data_exists = false;
	}
	
	public void CheckUnsavedData() {
		if (_unsaved_data_exists) {
			if (Globals.GetUserConfirmation(
					"Current journal contains unsaved data, would you like to save? (y/n)")){
				Save();
			}
		}

	}

	public void Display() {
		foreach (Entry entry in _contents){
			entry.Display();
		}
	}

	public string Serialize() {
		var serialized_data = "";
		foreach (Entry entry in _contents){
			serialized_data += entry.Serialize();
			serialized_data += "~";
		}
		serialized_data.Remove(serialized_data.Length - 1);
		return serialized_data;
	}

	public void LoadFromSerializedString(string data) {
		string[] substrs = data.Split("~");
		foreach (string substr in substrs) {
			if (substr == "") {
				continue;
			}
			// Console.WriteLine($"Current substr: {substr}");
			Entry entry = new Entry(false);
			if (entry.LoadFromSerializedString(substr)) {
				_contents.Add(entry);
			}
		}
	}
}

class Entry {
	public string _entry = "";
	public string _date = "";
	public string _prompt = "";

	public Entry(bool random = true) {
		if (random) {
			_prompt = Globals._prompts[Globals._rng.Next(0, Globals._prompts.Count)];
		}
	}

	public void GetFromUser() {
		Console.WriteLine(_prompt);
		_entry = Console.ReadLine().Replace("|", "");
		_date = DateTime.Now.ToShortDateString();
	}

	public void Display() {
		Console.WriteLine(_date);
		Console.WriteLine(_prompt);
		Console.WriteLine(_entry);
	}

	public string Serialize() {
		return $"{_date}|{_prompt}|{_entry}";
	}
	
	// returns whether or not the load was a success
	public bool LoadFromSerializedString(string data) {
		// expects data of the form "date|prompt|entry"
		// the parent journal is expected to remove the ~
		string[] substrs = data.Split("|");
		try {
			_date = substrs[0];
			_prompt = substrs[1];
			_entry = substrs[2];
			return true;
		} catch (Exception) {
			Console.WriteLine($"Error: Invalid journal data detected: {data}");
			Console.WriteLine("Parsing for entry failed, skipping...");
			return false;
		}
	}
}
