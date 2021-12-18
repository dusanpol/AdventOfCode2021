namespace AdventOfCode;

public class Day_01 : BaseDay
{
    private readonly string _input;

    public Day_01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        int[] depths = ParseDepths();

        int increaseCount = 0;

        for (int i = 1; i < depths.Length; i++)
        {
            int currentDepth = depths[i];
            int previousDepth = depths[i - 1];

            if (currentDepth > previousDepth)
                increaseCount++;
        }

        string solution = increaseCount.ToString();

        return ValueTask.FromResult(solution);
    }

    public override ValueTask<string> Solve_2()
    {
        int[] depths = ParseDepths();

        int increaseCount = 0;

        for (int i = 3; i < depths.Length; i++)
        {
            int currentWindow = depths.Skip(i - 2).Take(3).Sum();
            int previousWindow = depths.Skip(i - 3).Take(3).Sum();

            if (currentWindow > previousWindow)
                increaseCount++;
        }

        string solution = increaseCount.ToString();

        return ValueTask.FromResult(solution);
    }

    private int[] ParseDepths()
    {
        return _input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => int.Parse(line))
            .ToArray();
    }
}
