using System;
using System.IO;

class Program {
	private static int score = 0;
	private static List<Goal> goals = new List<Goal>();

    static void Main(string[] args) {
		while (true) {
			MainMenu();
		}
	}
	
	// Displays and handles the main menu
	static void MainMenu() {
		Console.WriteLine($"""
				You have {score} points

				Commands:
				1. (C)reate new goal
				2. (L)ist goals
				3. (S)ave goals to file
				4. L(o)ad goals from file
				5. (M)ark goal
				6. (Q)uit program
				"""
		);

		string input = Console.ReadLine().ToLowerInvariant();

		switch (input) {
			case "1":
			case "c":
			case "create":
				GoalMenu();
				break;
			case "2":
			case "l":
			case "list":
				ListGoals();
				break;
			case "3":
			case "s":
			case "save":
				if (goals.Count == 0) {
					Console.WriteLine("No goals to save");
					break;
				}
				string contents = "";
				foreach (Goal goal in goals) {
					contents += goal.Serialize();
				}
				contents += $"score|{score}";
				string filename = GetString("Filename (will overwrite):");
				bool success = SaveToFile(filename, contents);
				if (!success) {
					Console.WriteLine("Failed to save to file");
				}
				break;
			case "4":
			case "o":
			case "load":
				string load_filename = GetString("Filename");
				LoadFromFile(load_filename);
				break;
			case "5":
			case "m":
			case "mark":
				ListGoals();
				int index = GetInt("Which goal would you like to mark?");
				if (index > goals.Count) {
					Console.WriteLine("Invalid goal selected");
					break;
				} else if (index < 1) {
					Console.WriteLine("Please pick a number > 0");
					break;
				}
				Goal goal_to_mark = goals[index - 1];
				if (!goal_to_mark.IsComplete()) {
					goal_to_mark.Mark(ref score);
					Console.WriteLine($"You earned {goal_to_mark.GetPoints()} points");
				} else {
					Console.WriteLine("Goal is already complete!");
				}
				break;
			case "6":
			case "q":
			case "quit":
				Console.WriteLine("Goodbye");
				Environment.Exit(0);
				break;
			default:
				Console.WriteLine("Unrecognized command, returning");
				break;
		}
	}
	
	static void ListGoals() {
		int count = 1;
		Console.WriteLine("\nThe goals are:");
		foreach (Goal goal in goals) {
			Console.WriteLine($"{count}. {goal.Display()}");
			count += 1;
		}
		Console.Write("\n");
	}

	// Displays and handles the goal creation menu
	static void GoalMenu() {
		Console.WriteLine($"""
				Goal Types:

				1. (S)imple goal
				2. (I)nfinite goal
				3. (M)ulti goal

				4. (B)ack to main menu
				"""
		);

		string input = Console.ReadLine().ToLowerInvariant();
		
		string label = GetString("Goal Title:");
		string desc = GetString("Goal Description:");
		int points = GetInt("Points on completion");

		switch (input) {
			case "1":
			case "s":
			case "simple":
				var sg = new SingleGoal(label, desc, points);
				goals.Add(sg);
				break;
			case "2":
			case "i":
			case "infinite":
				var ig = new InfGoal(label, desc, points);
				goals.Add(ig);
				break;
			case "3":
			case "m":
			case "multi":
				int marks = GetInt("Times this goal can be completed");
				int bonus = GetInt("Bonus points on full completion:");
				var mg = new MultiGoal(label, desc, points, marks, bonus);
				goals.Add(mg);
				break;
			case "4":
			case "b":
			case "back":
				break;
			default:
				Console.WriteLine("Unrecognized command, returning");
				break;
		}

	}
	
	// Repeats until the user enters an integer
	static int GetInt(string prompt) {
		int num;
		while (true) {
			Console.WriteLine(prompt);
			if (int.TryParse(Console.ReadLine(), out num)) {
				return num;
			}
			Console.WriteLine("Please learn what a number is and try again");
		}
	}
	
	// Makes sure it won't contain illegal characters and break the parser
	static string GetString(string prompt) {
		Console.WriteLine(prompt);
		string str = Console.ReadLine();
		str.Replace("~", "");
		str.Replace("|", "");
		return str;
	}
	
	// Attempts to write contents to filename, overwriting if it exists
	static bool SaveToFile(string filename, string contents) {
		try {
			if (string.IsNullOrWhiteSpace(filename) ||
					filename != Path.GetFileName(filename)) {
				return false;
			}

			File.WriteAllText(filename, contents);
			return true;
		} catch (Exception) {
			return false;
		}
	}
	
	// Deserializes the overcomplicated inheritance structure
	static void LoadFromFile(string filename) {
		string data = "";
		try {
			data = File.ReadAllText(filename);
		} catch (Exception) {
			Console.WriteLine("Failed to read file, using empty string");
		}

		string[] goal_strings = data.Split("~");
		foreach (string goal_string in goal_strings) {
			string[] meta = goal_string.Split("|");
			switch (meta[0]) {
				case "single":
					try {
						var sg = new SingleGoal(meta[1], meta[2], int.Parse(meta[3]), bool.Parse(meta[4]));	
						goals.Add(sg);
					} catch (Exception) {
						goto default; // Mark me down points, I dare you
					}
					break;
				case "inf":
					try {
						var ig = new InfGoal(meta[1], meta[2], int.Parse(meta[3]), bool.Parse(meta[4]));
						goals.Add(ig);
					} catch (Exception) {
						goto default; // The evil and menacing goto statement
					}
					break;
				case "multi":
					try {
						var mg = new MultiGoal(meta[1], meta[2], int.Parse(meta[3]), int.Parse(meta[4]), int.Parse(meta[5]), bool.Parse(meta[6]));
						goals.Add(mg);
					} catch (Exception) {
						goto default;
					}
					break;
				case "score":
					try {
						score = int.Parse(meta[1]);
					} catch (Exception) {
						goto default;
					}
					break;
				default:
					Console.WriteLine("Unrecognized goal format, ignoring");
					break;
			}
		}
	}
}
