using System;

public class Scripture {
	private const int _MIN_HIDDEN_WORDS = 5;
	private const int _MAX_HIDDEN_WORDS = 20;

	private string _original_text;
	private List<Word> _words = new(); // never changes
	private List<Word> _visible_words = new(); // reference removed when hidden
	private Random _rng = new();
	private Reference _reference; // Reference class is also unnecessary bloat
	
	// Creates new Scripture (by Scripture we apparently mean paragraph
	// with hidable text, not actually a scripture, but I digress)
	// from a raw string of text

	public Scripture(Reference reference, Bible bible) {
		string text = bible.GetScriptureText(reference);
		_reference = reference;
		_original_text = text;
		string[] words = text.Split(" ");
		foreach (string word in words) {
			Word w = new Word(word);
			_words.Add(w);
			_visible_words.Add(w);
		}
	}
	
	public override string ToString() {
		string text = "";
		text += _reference.GetBook();
		text += " ";
		text += _reference.GetChapter().ToString();
		text += ":";
		text += _reference.GetStartVerse().ToString();
		if (_reference.GetStartVerse() != _reference.GetEndVerse()) {
			text += "-";
			text += _reference.GetEndVerse();
		}
		text += "\n\n";

		foreach (Word word in _words) {
			text += word.ToString();
			text += " ";
		}

		return text;
	}
	
	// Have to be careful here; can't foreach a list and mess with the indices.
	// Function returns true if all words are hidden afterwards.
	public bool HideNumWords(int num) {
		num = Math.Clamp(num, _MIN_HIDDEN_WORDS, _MAX_HIDDEN_WORDS);
		for (int i = 0; i < num; i ++) {
			if (_visible_words.Count == 0){
				return true;
			}
			int idx = _rng.Next(_visible_words.Count);
			Word word = _visible_words[idx];
			word.ToggleHidden();
			_visible_words.RemoveAt(idx);
		}
		return false;
	}
	
}
