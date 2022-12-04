namespace AdventOfCode.Challenges;

public class Day4 : DayBase
{
    private readonly string[] _inputs;

    public Day4()
    {
        _inputs = ReadFile("Day4.txt");
    }

    public override void Start()
    {
        Part1();
        Part2();
    }

    private void Part1()
    {
        int duplicateEfforts = 0;
        foreach (string sections in _inputs)
        {
            int[][] areaLimits = sections.Split(',')
                .Select(x => x.Split('-')
                    .Select(int.Parse).ToArray())
                .ToArray();

            if (SecondContainsFirst(areaLimits) || FirstContainsSecond(areaLimits))
            {
                duplicateEfforts++;
            }
        }
        
        Console.WriteLine(duplicateEfforts);
    }
    
    private void Part2()
    {
        int overlaps = 0;
        foreach (string sections in _inputs)
        {
            int[][] areaLimits = sections.Split(',')
                .Select(x => x.Split('-')
                    .Select(int.Parse).ToArray())
                .ToArray();

            if (AreasOverlap(areaLimits))
            {
                overlaps++;
            }
        }
        
        Console.WriteLine(overlaps);
    }

    private static bool AreasOverlap(IReadOnlyList<int[]> areaLimits)
    {
        return (areaLimits[0][0] >= areaLimits[1][0] && areaLimits[0][0] <= areaLimits[1][1])
               || (areaLimits[1][0] >= areaLimits[0][0] && areaLimits[1][0] <= areaLimits[0][1])
               || (areaLimits[0][1] >= areaLimits[1][0] && areaLimits[0][1] <= areaLimits[1][1])
               || (areaLimits[1][1] >= areaLimits[0][0] && areaLimits[1][1] <= areaLimits[0][1]);
    }

    private static bool SecondContainsFirst(IReadOnlyList<int[]> areaLimits)
    {
        return areaLimits[0][0] >= areaLimits[1][0] && areaLimits[0][1] <= areaLimits[1][1];
    }
    
    private static bool FirstContainsSecond(IReadOnlyList<int[]> areaLimits)
    {
        return areaLimits[0][0] <= areaLimits[1][0] && areaLimits[0][1] >= areaLimits[1][1];
    }
}