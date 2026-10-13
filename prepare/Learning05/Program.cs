using System;

class Program {
    static void Main(string[] args) {
		var s = new Square(5, "pink");
		var r = new Rectangle(5, 4, "blue");
		var c = new Circle(6, "green");

		var shapes = new List<Shape>();
		shapes.Add(s);
		shapes.Add(r);
		shapes.Add(c);

		foreach (Shape shape in shapes) {
			Console.WriteLine(shape.GetColor());
			Console.WriteLine(shape.GetArea());
		}

    }
}
