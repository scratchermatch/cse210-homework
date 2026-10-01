// Genuinely useless class. This could all not exist and I would literally
// just pass 3 arguments to a different class.
//
// INSTEAD, ONLY FOR GRADING PURPOSES:
// Write a bunch of mandated private attributes
// Write a bunch of boilerplate constructors
// Write a bunch of getters that literally just return the private variable
// 30 lines of boilerplate, zero proven benefit.
//
// THIS IS LITERALLY JUST AIR AND FLUFF.

using System;

public class Reference {
	private string _book;
	private int _chapter;
	private int _start_verse;
	private int _end_verse;

	public Reference(string book, int chapter, int verse) {
		_book = book;
		_chapter = chapter;
		_start_verse = verse;
		_end_verse = verse;
	}

	public Reference(string book, int chapter, int start_verse, int end_verse) {
		_book = book;
		_chapter = chapter;
		_start_verse = start_verse;
		_end_verse = end_verse;
	}
	
	// I'm cringing incredibly hard right now
	public string GetBook() {
		return _book;
	}

	public int GetChapter() {
		return _chapter;
	}

	public int GetStartVerse() {
		return _start_verse;
	}

	public int GetEndVerse(){
		return _end_verse;
	}
}
