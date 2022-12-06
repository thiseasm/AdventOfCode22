namespace AdventOfCode.Challenges;

public class Day6 : DayBase
{
    private readonly string _input;

    public Day6()
    {
        _input = ReadText("Day6.txt");
    }
    public override void Start()
    {
        Part1();
        Part2();
    }

    private void Part1()
    {
        List<char> marker = new();
        char[] datastream = _input.ToCharArray();
        marker.AddRange(datastream.Take(3));
        for (int counter = 3; counter < datastream.Length; counter++)
        {
            marker.Add(datastream[counter]);
            if (marker.Distinct().Count() == marker.Count)
            {
                Console.WriteLine(counter+1);
                break;
            }
            marker.RemoveAt(0);
        }
    }
    
    private void Part2()
    {
        List<char> marker = new();
        char[] datastream = _input.ToCharArray();
        marker.AddRange(datastream.Take(13));
        for (int counter = 13; counter < datastream.Length; counter++)
        {
            marker.Add(datastream[counter]);
            if (marker.Distinct().Count() == marker.Count)
            {
                Console.WriteLine(counter+1);
                break;
            }
            marker.RemoveAt(0);
        }
    }
}