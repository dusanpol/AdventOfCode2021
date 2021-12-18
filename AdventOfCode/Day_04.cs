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
        foreach (var drawnNumber in _numbers)
        {
            foreach (var board in _boards)
                board.Mark(drawnNumber);
            
            var winingBoard = _boards.FirstOrDefault(board => board.IsWinning);

            if (winingBoard != null)
            {   
                var score = winingBoard.CalculateScore(drawnNumber);
                var solution = score.ToString();
                return ValueTask.FromResult(solution);
            }
        }

        throw new InvalidDataException("There is no winner!");
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }

    private class Board
    {
        public Number[][] Numbers { get; }

        public bool IsWinning
        {
            get
            {
                bool anyRowWinning = Numbers.Any(row => row.All(num => num.Marked));
                bool anyColumnWinning = Enumerable
                    .Range(0, 5)
                    .Any(row => Enumerable
                        .Range(0, 5)
                        .All(col => Numbers[col][row].Marked));

                return anyRowWinning || anyColumnWinning;
            }
        }

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
                .SelectMany(num => num)
                .Where(num => num.Value == number)
                .ToArray();

            foreach (var targetNumber in targetNumbers)
                targetNumber.Marked = true;
        }

        public int CalculateScore(int drawnNumber)
        {
            int unmarkedSum = Numbers
                .SelectMany(num => num)
                .Where(num => !num.Marked)
                .Sum(num => num.Value);

            return unmarkedSum * drawnNumber;
        }
    }

    private class Number
    {
        public int Value { get; }

        public bool Marked { get; set; } = false;

        public Number(int value)
        {
            Value = value;
        }

        public override string ToString() => Marked ? $"{Value} Marked" : Value.ToString();
    }
}
