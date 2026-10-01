using System;

public class Word {
	// It would be confusing to the user if they thought a word
	// was 5 letters long but actually it was 4 letters long with
	// a comma at the end.
	private const string _PUNCTUATION = ",.-?![]():;\'\"&~";

	private bool _hidden;
	private string _text;

	public Word(string text, bool hidden = false) {
		_text = text;
		_hidden = hidden;
	}

	public void ToggleHidden() {
		_hidden = !_hidden;
	}
	
	public bool IsHidden() {
		return _hidden;
	}

	public override string ToString() {
		if (!_hidden) {
			return _text;
		} else {
			string censored_text = "";
			foreach (char character in _text) {
				if (_PUNCTUATION.Contains(character)) {
					censored_text += character;
				} else {
					censored_text += "_";
				}
			}

			return censored_text;
		}
	}
}
