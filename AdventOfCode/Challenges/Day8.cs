namespace AdventOfCode.Challenges;

public class Day8 : DayBase
{
    private string[] _inputs { get; set; }

    public Day8()
    {
        _inputs = ReadFile("Day8.txt");
    }
    public override void Start()
    {
        Part1();
        Part2();
    }

    private void Part2()
    {
        int[,] grid = PopulateGrid();

        long highestScore = 0;
        for(int x = 0; x < _inputs.Length; x++)
        {
            for(int y = 0; y < _inputs[0].Length; y++)
            {
                int[] row = GetRow(grid, x);
                int[] column = GetColumn(grid, y);
                
                List<int> left = x != 0 ? row.ToList().GetRange(0, y) : new List<int>();
                left.Reverse();
                List<int> right = x != _inputs.Length ? row.Skip(y + 1).ToList() : new List<int>();
                
                List<int> up = y != 0 ? column.ToList().GetRange(0, x) : new List<int>();
                up.Reverse();
                List<int> down = y != _inputs[0].Length ? column.Skip(x + 1).ToList() : new List<int>();

                if (!left.Any() || !right.Any() || !up.Any() || !down.Any())
                {
                    continue;
                }

                int height = grid[x, y];
                int viewLeft = left.FindIndex(l => l >= height) < 0 ? left.Count : left.FindIndex(l => l >= height) + 1;
                int viewRight = right.FindIndex(r => r >= height) < 0 ? right.Count : right.FindIndex(r => r >= height) + 1;
                int viewUp = up.FindIndex(u => u >= height) < 0 ? up.Count : up.FindIndex(u => u >= height) + 1;
                int viewDown = down.FindIndex(d => d >= height) < 0 ? down.Count : down.FindIndex(d => d >= height) + 1;

                long score = viewDown * viewUp * viewRight * viewLeft;
                if (score > highestScore)
                {
                    highestScore = score;
                }
            }
        }
        
        Console.WriteLine(highestScore);
    }

    private void Part1()
    {
        int[,] grid = PopulateGrid();

        List<Tree> treesVisible = new();
        for (int x = 0; x < _inputs.Length; x++)
        {
            Tree outsideTree1 = new(x, 0);
            Tree outsideTree2 = new(x, _inputs[x].Length - 1);
            if (!treesVisible.Any(t => t.Position.Equals(outsideTree1.Position)))
            {
                treesVisible.Add(outsideTree1);
            }
            if (!treesVisible.Any(t => t.Position.Equals(outsideTree2.Position)))
            {
                treesVisible.Add(outsideTree2);
            }
        }
        
        for (int y = 0; y < _inputs[0].Length; y++)
        {
            Tree outsideTree1 = new(0, y);
            Tree outsideTree2 = new(_inputs.Length - 1, y);
            if (!treesVisible.Any(t => t.Position.Equals(outsideTree1.Position)))
            {
                treesVisible.Add(outsideTree1);
            }
            if (!treesVisible.Any(t => t.Position.Equals(outsideTree2.Position)))
            {
                treesVisible.Add(outsideTree2);
            }
        }
        
        //rows
        for (int x = 0; x < _inputs.Length; x++)
        {
            List<int> row = new();
            for (int y = 0; y < _inputs[0].Length; y++)
            {
                row.Add(grid[x,y]);
            }

            int maxHeight = row.First();
            for (int y = 1; y < _inputs[0].Length; y++)
            {
                if (row[y] > maxHeight)
                {
                    maxHeight = row[y];
                    Tree tree = new(x, y);
                    if (!treesVisible.Any(t => t.Position.Equals(tree.Position)))
                    {
                        treesVisible.Add(tree);
                    }
                }
            }
            
            maxHeight = row.Last();
            for (int y = _inputs[0].Length - 1; y >= 0; y--)
            {
                if (row[y] > maxHeight)
                {
                    maxHeight = row[y];
                    Tree tree = new(x,  y);
                    if (!treesVisible.Any(t => t.Position.Equals(tree.Position)))
                    {
                        treesVisible.Add(tree);
                    }
                }
            }
        }
        
        //columns
        for (int y = 0; y < _inputs[0].Length; y++)
        {
            List<int> column = new();
            for (int x = 0; x < _inputs.Length; x++)
            {
                column.Add(grid[x,y]);
            }

            int maxHeight = column.First();
            for (int x = 1; x < _inputs.Length; x++)
            {
                if (column[x] > maxHeight)
                {
                    maxHeight = column[x];
                    Tree tree = new(x, y);
                    if (!treesVisible.Any(t => t.Position.Equals(tree.Position)))
                    {
                        treesVisible.Add(tree);
                    }
                }
            }
            
            maxHeight = column.Last();
            for (int x = _inputs.Length - 1; x >= 0; x--)
            {
                if (column[x] > maxHeight)
                {
                    maxHeight = column[x];
                    Tree tree = new(x, y);
                    if (!treesVisible.Any(t => t.Position.Equals(tree.Position)))
                    {
                        treesVisible.Add(tree);
                    }
                }
            }
        }
        
        
        Console.WriteLine(treesVisible.Count);
    }

    private int[,] PopulateGrid()
    {
        int[,] grid = new int[_inputs.Length, _inputs[0].Length];
        for (int x = 0; x < _inputs.Length; x++)
        {
            for (int y = 0; y < _inputs[x].Length; y++)
            {
                grid[x, y] = int.Parse(_inputs[x][y].ToString());
            }
        }

        return grid;
    }
    
   
    public int[] GetColumn(int[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
            .Select(x => matrix[x, columnNumber])
            .ToArray();
    }

    public int[] GetRow(int[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
            .Select(x => matrix[rowNumber, x])
            .ToArray();
    }
    
}

public struct Tree
{
    public Tree(int x, int y)
    {
        Position = $"{x},{y}";
    }
    
    public string Position { get; set; }
}