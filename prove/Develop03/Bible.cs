using System.IO;

public class Bible {
	// each line is reference followed by verse
	string[] _raw_data;
	// strong typing at its worst
	Dictionary<string, Dictionary<int, Dictionary<int, string>>> _parsed_data
		= new();

	public Bible(string filepath) {
		_raw_data = File.ReadAllLines(filepath);
		ParseRawData();
	}

	public string GetScriptureText(Reference r) {
		var text = "";
		// If only the rubric didn't force you to make a fluff Reference class
		// Then this would look pretty.
		for (var i = r.GetStartVerse(); i < r.GetEndVerse() + 1; i++){
			text += _parsed_data[r.GetBook()][r.GetChapter()][i];
			text += " ";
		}
		return text;
	}

	// Returns true on success, false on error
	private bool ParseRawData() {
		foreach (string line in _raw_data) {
			string[] segments = line.Split("\t");
			if (segments.Length < 2) {
				Console.WriteLine($"Parser: Invalid line: {line}");
				continue;
			}
			string book = String.Join(" ", segments[0].Split(" ")[..^1]); // Genesis
			string[] numbers = segments[0].Split(" ")[^1].Split(":");//[11, 12]
			if (!int.TryParse(numbers[0], out int chapter)) {
				Console.WriteLine($"Parser: Invalid chapter: {numbers[0]}");
				continue;
			}
			if (!int.TryParse(numbers[1], out int verse)){
				Console.WriteLine($"Parser: Invalid verse: {numbers[1]}");
				continue;
			}
			string text = segments[1];
			
			AddText(book, chapter, verse, text);
		}

		return true;
	}
	
	// Huge gripe with C#:
	// If a key doesn't exist, the dictionary should CREATE IT instead of
	// THROWING A TEMPER TANTRUM LIKE A TODDLER
	private void AddText(string book, int chapter, int verse, string text) {
		if (!_parsed_data.TryGetValue(book, out var chapters)) {
		    chapters = new Dictionary<int, Dictionary<int, string>>();
		    _parsed_data[book] = chapters;
		}

		if (!chapters.TryGetValue(chapter, out var verses)) {
		    verses = new Dictionary<int, string>();
		    chapters[chapter] = verses;
		}

		verses[verse] = text;
	}
	
}
