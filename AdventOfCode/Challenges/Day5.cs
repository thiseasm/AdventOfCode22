namespace AdventOfCode.Challenges;

public class Day5 : DayBase
{
    private readonly string[] _inputs;

    public Day5()
    {
        _inputs = ReadFile("Day5.txt");
    }

    public override void Start()
    {
        Part1();
        Part2();
    }

    private void Part1()
    {
        int separator = 0;
        for (int counter = 0; counter < _inputs.Length; counter++)
        {
            if (_inputs[counter].Equals(string.Empty))
            {
                separator = counter;
                break;
            }
        }

        string[] startingConfig = _inputs[..separator];
        string[] commands = _inputs[(separator + 1)..];

        int numberOfStacks = startingConfig[^1].ToCharArray().Where(x => !x.Equals(' '))
            .Select(x => int.Parse(x.ToString())).Max();

        List<Stack<char>> crateStacks = GetStartingStacks(startingConfig,numberOfStacks, separator);
        foreach (string moveCommand in commands)
        {
            Command command = ParseCommand(moveCommand);
            for (int counter = 1; counter <= command.CratesToMove; counter++)
            {
                char crate = crateStacks[command.Source].Pop();
                crateStacks[command.Target].Push(crate);
            }
        }

        string topCrates = new(crateStacks.Select(x => x.Peek()).ToArray());
        Console.WriteLine(topCrates);

    }
    
    private void Part2()
    {
        int separator = 0;
        for (int counter = 0; counter < _inputs.Length; counter++)
        {
            if (_inputs[counter].Equals(string.Empty))
            {
                separator = counter;
                break;
            }
        }

        string[] startingConfig = _inputs[..separator];
        string[] commands = _inputs[(separator + 1)..];

        int numberOfStacks = startingConfig[^1].ToCharArray().Where(x => !x.Equals(' '))
            .Select(x => int.Parse(x.ToString())).Max();

        List<Stack<char>> crateStacks = GetStartingStacks(startingConfig,numberOfStacks, separator);
        foreach (string moveCommand in commands)
        {
            Command command = ParseCommand(moveCommand);
            List<char> cratesToMove = new();
            for (int counter = 1; counter <= command.CratesToMove; counter++)
            {
                char crate = crateStacks[command.Source].Pop();
                cratesToMove.Add(crate);
            }

            cratesToMove.Reverse();
            foreach (char crate in cratesToMove)
            {
                crateStacks[command.Target].Push(crate);
            }
            cratesToMove = new();
        }

        string topCrates = new(crateStacks.Select(x => x.Peek()).ToArray());
        Console.WriteLine(topCrates);

    }

    private static Command ParseCommand(string moveCommand)
    {
        List<int> parsedCommand = moveCommand
            .Split(' ')
            .Where(x => int.TryParse(x, out int _))
            .Select(int.Parse)
            .ToList();

        return new Command
        {
            CratesToMove = parsedCommand[0],
            Source = parsedCommand[1] - 1,
            Target = parsedCommand[2] - 1
        };
    }

    private static List<Stack<char>> GetStartingStacks(IReadOnlyList<string> startingConfig,int numberOfStacks, int separator)
    {
        List<Stack<char>> crateStacks = new();
        for (int counter = 1; counter <= numberOfStacks; counter++)
        {
            Stack<char> stack = new();
            for (int position = separator - 2; position >= -1; position--)
            {
                if (position == -1)
                {
                    crateStacks.Add(stack);
                    break;
                }

                int positionOfStack = startingConfig[separator - 1].IndexOf(char.Parse(counter.ToString()));
                char crateName = startingConfig[position][positionOfStack];
                if (crateName.Equals(' '))
                {
                    crateStacks.Add(stack);
                    break;
                }

                stack.Push(crateName);
            }
        }

        return crateStacks;
    }
}

internal struct Command
{
    public int CratesToMove { get; set; }
    public int Source { get; set; }
    public int Target { get; set; }
}