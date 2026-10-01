public class Fraction {
	// First of all, why aren't we doing this:
	// public int Numerator // property
	// {get; set;}
	// public int Denominator;
	// {get; set;}
	//
	// Also, getters and setters should basically never be used
	// It's beyond me why we are teaching them
	// 
	// bad OOP practice, bad design...
	
	private int _numerator;
	private int _denominator;

	public int Numerator {
		get { return _numerator; }  // get method
		set { _numerator = value; } // set method
	}

	public int Denominator {
		get { return _denominator; }
		set { _denominator = value; }
	}
	
	// Empty constructor, initialize to 1/1
	public Fraction(){
		Numerator = 1;
		Denominator = 1;
	}
	
	// Given a whole number, initialize to w/1
	public Fraction(int w){
		Numerator = w;
		Denominator = 1;
	}
	
	// Given n and d, intialize to n/d
	public Fraction(int n, int d){
		Numerator = n;
		Denominator = d;
	}

	// Returns the fraction as a string of n/d
	public string GetFractionString(){
		return $"{Numerator}/{Denominator}";
	}
	
	// Divides the fraction and returns a double
	public double GetFractionDecimal(){
		return (double)Numerator/(double)Denominator; 
		// ye olde integer division
	}
}
