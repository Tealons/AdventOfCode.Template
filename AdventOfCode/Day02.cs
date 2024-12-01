namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string _input;
    private readonly List<string> _lines;
    private readonly List<string> _part1;
    private readonly List<string> _part2;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
        _lines = _input.Split(Environment.NewLine).ToList();
        foreach (var line in _lines)
        {
            //substring
            //_part1.Add(line.Substring(0,1)); //first char
            //_part2.Add(line.Substring(1)); //rest

            //split on char
            //var parts = line.Split(',');
            //_part1.Add(parts[0]);
            //_part2.Add(parts[1]);
        }
    }

    public override ValueTask<string> Solve_1() => new($"{Solve1()}");

    public override ValueTask<string> Solve_2() => new($"{Solve2()}");

    public long Solve2()
    {
        
        return 0;
    }


    public long Solve1()
    {
        return 0;
    }
}
