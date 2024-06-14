using Vectors;

internal class Program
{
    private static void Main(string[] args)
    {
        var vector = new Vector([1, 2, 3]);
        Console.WriteLine(vector);
        vector.Count = 1;
        Console.WriteLine(vector);
        vector.Count = 5;
        Console.WriteLine(vector);
    }
}