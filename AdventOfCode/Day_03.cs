namespace AdventOfCode;

public class Day_03 : BaseDay
{
    private readonly string[] _inputLines;
    private readonly int _digitCount;
    
    public Day_03()
    {
        string[] inputLines = File
            .ReadAllText(InputFilePath)
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .ToArray();

        _digitCount = inputLines[0].Length;

        // check input for incorrect digit count and that is binary
        if (inputLines.Any(line => line.Length != _digitCount || !line.All(ch => ch == '0' || ch == '1')))
            throw new InvalidDataException();

        _inputLines = inputLines;
    }

    public override ValueTask<string> Solve_1()
    {
        int gamma = 0;
        int epsilon = 0;

        // iterate digits from most significant to least significant (in binary form!)
        for (int d = 0; d < _digitCount; d++)
        {
            int oneBitCount = _inputLines.Where(l => l[d] == '1').Count();
            int zeroBitCount = _inputLines.Length - oneBitCount; // no need to iterate again

            bool setGammaBit = oneBitCount > zeroBitCount; // set current gamma digit to 1, if it's the most common?
            bool setEpsilonBit = !setGammaBit; // the least common epsilon bit is the opposite of gamma bit

            // since we are iterating from most significant bit, we need index base: digitCount - 1
            // and shift to current digit index
            int digitIndex = _digitCount - 1 - d;

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
        int oxygenRating = 0;
        string[] oxygenNumbers = _inputLines;

        // iterate digits from most significant to least significant (in binary form!)
        for (int d = 0; d < _digitCount; d++)
        {
            int oneBitCount = oxygenNumbers.Where(l => l[d] == '1').Count();
            int zeroBitCount = oxygenNumbers.Length - oneBitCount; // no need to iterate again
            char oxygenBit;

            if (oneBitCount == zeroBitCount)
                oxygenBit = '1';
            else
                oxygenBit = oneBitCount > zeroBitCount ? '1' : '0';

            oxygenNumbers = oxygenNumbers
                .Where(n => n[d] == oxygenBit)
                .ToArray();

            if (oxygenNumbers.Length == 1)
            {
                oxygenRating = Convert.ToInt32(oxygenNumbers.First(), 2);
                break;
            }
        }

        int scrubberRating = 0;
        string[] scrubberNumbers = _inputLines;

        // iterate digits from most significant to least significant (in binary form!)
        for (int d = 0; d < _digitCount; d++)
        {
            int oneBitCount = scrubberNumbers.Where(l => l[d] == '1').Count();
            int zeroBitCount = scrubberNumbers.Length - oneBitCount; // no need to iterate again
            char scrubberBit;

            if (oneBitCount == zeroBitCount)
            {
                scrubberBit = '0';
            }
            else
            {
                scrubberBit = oneBitCount > zeroBitCount ? '0' : '1';
            }

            scrubberNumbers = scrubberNumbers
                .Where(n => n[d] == scrubberBit)
                .ToArray();
            
            if (scrubberNumbers.Length == 1)
            {
                scrubberRating = Convert.ToInt32(scrubberNumbers.First(), 2);
                break;
            }
        }

        int lifeSupportRating = oxygenRating * scrubberRating;
        var solution = lifeSupportRating.ToString();
        return ValueTask.FromResult(solution);
    }
}