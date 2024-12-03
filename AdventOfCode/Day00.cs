using AoC_Helper_Methods.Algorithms;

namespace AdventOfCode;

public class Day00 : BaseDay
{
    private readonly string _input;
    private readonly List<string> _lines;
    private readonly List<string> _part1;
    private readonly List<string> _part2;

    public Day00()
    {
        _input = File.ReadAllText(InputFilePath);
        _lines = _input.Split(Environment.NewLine).ToList();

    }

    public override ValueTask<string> Solve_1() => new($"{Solve1()}");

    public override ValueTask<string> Solve_2() => new($"{Solve2()}");

    public long Solve2()
    {

        return 0;

    }


    public long Solve1()
    {

        Dijkstra2.Letsparty(_lines);
        return 0;
    }
}
