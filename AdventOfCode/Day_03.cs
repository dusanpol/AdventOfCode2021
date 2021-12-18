namespace AdventOfCode;

public class Day_03 : BaseDay
{
    private readonly string _input;

    public Day_03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var lines = ParseLines();
        int digitCount = lines[0].Length;
        
        // check input for incorrect digit count and that is binary
        if (lines.Any(line => line.Length != digitCount || !line.All(ch => ch == '0' || ch == '1')))
            throw new InvalidDataException();

        int gamma = 0;
        int epsilon = 0;

        // iterate digits from most significant to least significant (in binary form!)
        for (int d = 0; d < digitCount; d++)
        {
            int oneBitCount = lines.Where(l => l[d] == '1').Count();
            int zeroBitCount = lines.Length - oneBitCount; // no need to iterate again

            bool setGammaBit = oneBitCount > zeroBitCount; // set current gamma digit to 1, if it's the most common?
            bool setEpsilonBit = !setGammaBit; // the least common epsilon bit is the opposite of gamma bit

            // since we are iterating from most significant bit, we need index base: digitCount - 1
            // and shift to current digit index
            int digitIndex = digitCount - 1 - d;

            if (setGammaBit)
            {
                int mask = 1 << digitIndex;
                gamma |= mask;
            }

            if (setEpsilonBit)
            {
                int mask = 1 << digitIndex;
                epsilon |= mask;
            }
        }

        int consumption = gamma * epsilon;

        var solution = consumption.ToString();
        return ValueTask.FromResult(solution);
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }

    private string[] ParseLines()
    {
        return _input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .ToArray();
    }
}