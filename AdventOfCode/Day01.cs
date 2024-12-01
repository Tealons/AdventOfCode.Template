namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string _input;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{Solve1()}");

    public override ValueTask<string> Solve_2() => new($"{Solve2()}");

    public long Solve2()
    {
        long runningTotal = 0;
        var list1 = new List<long>();
        var list2 = new List<long>();
        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var day = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            list1.Add(long.Parse(day[0]));
            list2.Add((long.Parse(day[1])));
        }

        var i = 0;
        foreach (var number1 in list1)
        {
            var count = list2.Count(x => x == number1);
            long multiple = number1 * count;
            runningTotal += multiple;
            i++;
        }
        return runningTotal;
    }


    public long Solve1()
    {
        long runningTotal = 0;
        var list1 = new List<long>();
        var list2 = new List<long>();
        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var day = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            list1.Add(long.Parse(day[0]));
            list2.Add((long.Parse(day[1])));
        }

        list1 = list1.OrderBy(x => x).ToList();
        list2 = list2.OrderBy(x => x).ToList();
        var i = 0;
        foreach (var number1 in list1)
        {
            var difference = Math.Abs(list2[i] - number1);
            runningTotal += difference;
            i++;
        }
        return runningTotal;
    }
}
