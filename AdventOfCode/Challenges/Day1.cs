namespace AdventOfCode.Challenges;

public class Day1 : DayBase
{
    private readonly int[] _inputs;

    public Day1()
    {
        _inputs = ReadSpaceSeparatedIntegers("Day1.txt");
    }
    
    public override void Start()
    {
        Part1();
        Part2();
    }

    private void Part1()
    {
        List<int> caloriesPerElf = new List<int>();
        int caloriesOfEachElf = 0;
        foreach (var meal in _inputs)
        {
            if (meal == -1)
            {
                caloriesPerElf.Add(caloriesOfEachElf);
                caloriesOfEachElf = 0;
                continue;
            }

            caloriesOfEachElf += meal;
        }
        
        Console.WriteLine(caloriesPerElf.Max());
    }
    
    private void Part2()
    {
        List<int> caloriesPerElf = new();
        int caloriesOfEachElf = 0;
        foreach (var meal in _inputs)
        {
            if (meal == -1)
            {
                caloriesPerElf.Add(caloriesOfEachElf);
                caloriesOfEachElf = 0;
                continue;
            }

            caloriesOfEachElf += meal;
        }

        long caloriesTotal = caloriesPerElf.OrderByDescending(x => x).Take(3).Sum();

        Console.WriteLine(caloriesTotal);
    }
}