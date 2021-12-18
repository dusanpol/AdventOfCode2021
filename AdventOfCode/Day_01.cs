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
        var depths = _input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => int.Parse(line))
            .ToArray();

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

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2");
}
