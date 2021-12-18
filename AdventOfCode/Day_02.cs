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
            var commands = ParseSubCommands();

            int distance = 0;
            int depth = 0;

            foreach (var command in commands)
            {
                switch (command.Type)
                {
                    case SubCmdType.Forward:
                        distance += command.Value;
                        break;

                    case SubCmdType.Up:
                        depth -= command.Value;
                        break;

                    case SubCmdType.Down:
                        depth += command.Value;
                        break;
                }
            }

            var solution = (distance * depth).ToString();
            return ValueTask.FromResult(solution);
        }

        public override ValueTask<string> Solve_2()
        {
            throw new NotImplementedException();
        }

        private SubCmd[] ParseSubCommands()
        {
            return _input
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(' '))
                .Select(lineParts =>
                {
                    var commandType = Enum.Parse<SubCmdType>(lineParts[0], true);
                    int commandValue = int.Parse(lineParts[1]);

                    return new SubCmd(commandType, commandValue);
                })
                .ToArray();
        }

        private struct SubCmd
        {
            public SubCmdType Type { get; }

            public int Value { get; set; }

            public SubCmd(SubCmdType type, int value)
            {
                Type = type;
                Value = value;
            }
        }

        private enum SubCmdType
        {
            Forward,
            Down,
            Up
        }
    }
}