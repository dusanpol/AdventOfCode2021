namespace AdventOfCode;

public class Day_04 : BaseDay
{
    private readonly int[] _numbers;
    private readonly Board[] _boards;

    public Day_04()
    {
        var input = File.ReadAllLines(InputFilePath);

        _numbers = input
            .First()
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => int.Parse(s))
            .ToArray();

        _boards = input
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Skip(1)
            .Chunk(5)
            .Select(x => new Board(x))
            .ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        throw new NotImplementedException();
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }

    private class Board
    {
        public Number[][] Numbers { get; }

        public Board(string[] inputLines)
        {
            Numbers = inputLines
                .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(ch => int.Parse(ch)))
                .Select(numbers => numbers.Select(n => new Number(n)).ToArray())
                .ToArray();
        }

        public void Mark(int number)
        {
            var targetNumbers = Numbers
                .SelectMany(x => x)
                .Where(n => n.Value == number && !n.Marked)
                .ToArray();

            foreach (var targetNumber in targetNumbers)
                targetNumber.Mark();
        }
    }

    private struct Number
    {
        public int Value { get; }

        public bool Marked { get; set; } = false;

        public Number(int value)
        {
            Value = value;
        }

        public void Mark() => Marked = true;

        public override string ToString() => Marked ? $"{Value} Marked" : Value.ToString();
    }
}
