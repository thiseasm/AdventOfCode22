namespace AdventOfCode.Challenges;

public abstract class DayBase
{
    public abstract void Start();

    private static readonly string _directory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"Inputs\");

    protected string[] ReadFile(string file)
    {
        string inputPath = Path.Combine(_directory, file);
        return File.ReadAllLines(inputPath);
    }
    protected string ReadText(string file)
    {
        string inputPath = Path.Combine(_directory, file);
        return File.ReadAllText(inputPath);
    }
    protected int[] ReadIntegerFile(string file)
    {
        string inputPath = Path.Combine(_directory, file);
        string[] inputsRaw = File.ReadAllLines(inputPath);

        return inputsRaw.Select(input => int.Parse(input.Trim())).ToArray();
    }

    protected long[] ReadLongFile(string file)
    {
        string inputPath = Path.Combine(_directory, file);
        string[] inputsRaw = File.ReadAllLines(inputPath);

        return inputsRaw.Select(input => long.Parse(input.Trim())).ToArray();
    }

    protected int[] ReadCsv(string file)
    {
        return ReadSv(file, ',');
    }

    protected int[] ReadSv(string file, char separator)
    {
        string inputPath = Path.Combine(_directory, file);
        string[] inputRaw = File.ReadAllText(inputPath).Split(separator);

        return inputRaw.Select(input => int.Parse(input.Trim())).ToArray();
    }
    
    protected int[] ReadSpaceSeparatedIntegers(string file)
    {
        string inputPath = Path.Combine(_directory, file);
        string[] inputsRaw = File.ReadAllLines(inputPath);

        return inputsRaw.Select(input => input.Equals(string.Empty) 
            ? -1
            :int.Parse(input.Trim())
        ).ToArray();
    }
}