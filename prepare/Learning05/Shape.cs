public abstract class Shape {
	private string _color;
	// Genuinely this get/set could just go in the constructor
	// and then you wouldn't need a get/set
	
	// Like so:
	public Shape(string color) {
		_color = color;
	}
	
	public string GetColor() { return _color; }
	public void SetColor(string color) { _color = color; }
	// no validation? WHY do they exit??
	
	// This parent class provides essentially zero value
	// If all you are doing is storing a color
	// ...
	// just declare 3 variables in the 3 other classes and I
	// GUARANTEE it is easier than this

	public abstract double GetArea();

	// Like, to an extent I understand that the idea is to teach
	// polymorphism and that this isn't a representative real
	// life example.
	//
	// But in real life, you shouldn't use inheritance in 99%
	// of cases.
}
