using System;

class Program
{
    static void Main(string[] args)
    {
	// I've been scammed
        // printf("Hello Discount C!");
    	Console.Write("What is your first name? ");
	string first_name = Console.ReadLine();
	Console.Write("What is your last name? ");
	string last_name = Console.ReadLine();

	Console.Write($"\nYour name is {last_name}, {first_name} {last_name}.");
    }
}
