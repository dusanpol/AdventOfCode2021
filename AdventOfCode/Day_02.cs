namespace AdventOfCode
{
    public class Day_02 : BaseDay
    {
        private readonly string _input;

        public Day_02()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            throw new NotImplementedException();
        }

        public override ValueTask<string> Solve_2()
        {
            throw new NotImplementedException();
        }

        private Tuple<SubCmd, int>[] ParseSubCommands()
        {
            return _input
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(' '))
                .Select(lineParts =>
                {
                    var command = Enum.Parse<SubCmd>(lineParts[0], true);
                    int commandValue = int.Parse(lineParts[1]);

                    return new Tuple<SubCmd, int>(command, commandValue);
                })
                .ToArray();
        }

        private enum SubCmd
        {
            Forward,
            Down,
            Up
        }
    }
}