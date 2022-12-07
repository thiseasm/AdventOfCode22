namespace AdventOfCode.Challenges;

public class Day7 : DayBase
{
    private string[] _inputs { get; set; }

    public Day7()
    {
        _inputs = ReadFile("Day7.txt");
    }
    public override void Start()
    {
        Part1();
        Part2();
    }

    private void Part1()
    {
        DeviceDirectory baseDirectory = new("base_directory");
        List<DeviceDirectory> systemDirectories = new() { baseDirectory };
        List<DeviceFile> files = new();
        MapDirectory(baseDirectory, systemDirectories, files);

        long totalSize = systemDirectories.Where(d => d.GetSize() <= 100000).Sum(x => x.GetSize());
        Console.WriteLine(totalSize);
    }
    
    private void Part2()
    {
        DeviceDirectory baseDirectory = new("base_directory");
        List<DeviceDirectory> systemDirectories = new() { baseDirectory };
        List<DeviceFile> files = new();
        MapDirectory(baseDirectory, systemDirectories, files);

        long emptySpace = 70000000 - baseDirectory.GetSize();
        long minSize = systemDirectories.Where( d => emptySpace + d.GetSize() >= 30000000).Min(x => x.GetSize());
        Console.WriteLine(minSize);
    }

    private void MapDirectory(DeviceDirectory baseDirectory, List<DeviceDirectory> systemDirectories, List<DeviceFile> files)
    {
        DeviceDirectory workingDirectory = baseDirectory;
        foreach (string line in _inputs)
        {
            if (line.StartsWith("$ cd "))
            {
                workingDirectory = GetWorkingDirectory(baseDirectory, systemDirectories, line, workingDirectory);
            }
            else if (line.StartsWith("dir"))
            {
                ParseDirectory(systemDirectories, line, workingDirectory);
            }
            else if (line.Equals("$ ls"))
            {
            }
            else
            {
                ParseFile(files, line, workingDirectory);
            }
        }
    }

    private static void ParseFile(List<DeviceFile> files, string line, DeviceDirectory workingDirectory)
    {
        string[] info = line.Split(' ');
        DeviceFile file;
        if (files.Exists(x => x.Name.Equals(info[1]) && x.Size.Equals(long.Parse(info[0]))))
        {
            file = files.Find(x => x.Name.Equals(info[1]) && x.Size.Equals(long.Parse(info[0])));
            files.Add(file);
        }
        else
        {
            file = new DeviceFile(info[0], info[1]);
        }

        if (!workingDirectory.Files.Any(x => x.Name.Equals(file.Name)))
        {
            workingDirectory.Files.Add(file);
        }
    }

    private static void ParseDirectory(List<DeviceDirectory> systemDirectories, string line, DeviceDirectory workingDirectory)
    {
        string name = line.Remove(0, 4) + $"-{workingDirectory.Name}";
        DeviceDirectory directory;
        if (systemDirectories.Any(x => x.Name.Equals(name)))
        {
            directory = systemDirectories.Find(x => x.Name.Equals(name));
        }
        else
        {
            directory = new DeviceDirectory(workingDirectory, name);
            systemDirectories.Add(directory);
        }

        if (!workingDirectory.Directories.Any(x => x.Name.Equals(directory.Name)))
        {
            workingDirectory.Directories.Add(directory);
        }
    }

    private static DeviceDirectory GetWorkingDirectory(DeviceDirectory baseDirectory, List<DeviceDirectory> systemDirectories, string line,DeviceDirectory workingDirectory )
    {
        if (line.EndsWith("/"))
        {
            return baseDirectory;
        }

        if (line.EndsWith(".."))
        {
            return workingDirectory.Parent;
        }

        string target = line.Remove(0, 5) + $"-{workingDirectory.Name}";
        return systemDirectories.Find(x => x.Name.Equals(target));
    }
}

public class DeviceDirectory
{
    public List<DeviceDirectory> Directories { get; set; }
    public DeviceDirectory? Parent { get; }
    public List<DeviceFile> Files { get; set; }
    public string Name { get; set; }

    public long GetSize() => Files.Sum(f => f.Size) + Directories.Sum(d => d.GetSize());

    public DeviceDirectory(DeviceDirectory parent, string name)
    {
        Parent = parent;
        Name = name;
        Directories = new List<DeviceDirectory>();
        Files = new List<DeviceFile>();
    }

    public DeviceDirectory(string name)
    {
        Name = name;
        Directories = new List<DeviceDirectory>();
        Files = new List<DeviceFile>();
    }
}

public class DeviceFile
{
    public long Size { get; private set; }
    public string Name { get; private set; }

    public DeviceFile(string size, string name)
    {
        Size = long.Parse(size);
        Name = name;
    }
}