using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string _input;
    private readonly List<string> _lines;
    private readonly List<string> _part1;
    private readonly List<string> _part2;
    private readonly string input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
        input = _input;
        _lines = _input.Split(Environment.NewLine).ToList();
        //foreach (var line in _lines)
        //{
        //    //substring
        //    //_part1.Add(line.Substring(0,1)); //first char
        //    //_part2.Add(line.Substring(1)); //rest

        //    //split on char
        //    //var parts = line.Split(',');
        //    //_part1.Add(parts[0]);
        //    //_part2.Add(parts[1]);
        //}
    }

    public override ValueTask<string> Solve_1() => new($"{Solve1()}");

    public override ValueTask<string> Solve_2() => new($"{Solve2()}");



    public long Solve1()
    {

        var runningtotal = 0;
        // Regular expression to match 'mul' with two digits inside parentheses
        string pattern = @"mul\((\d+),(\d+)\)";

        // Find all matches
        MatchCollection matches = Regex.Matches(_input, pattern);

        foreach (Match match in matches)
        {
            // Extract the digit groups (two groups per match)
            string firstNumber = match.Groups[1].Value;
            string secondNumber = match.Groups[2].Value;

            runningtotal += Int32.Parse(firstNumber) * Int32.Parse(secondNumber);
            //Console.WriteLine($"First Number: {firstNumber}, Second Number: {secondNumber}");
        }
        return runningtotal;

    }

    public long Solve2()
    {



        int p1 = 0;
        int p2 = 0;
        bool enabled = true;

        for (int i = 0; i < input.Length; i++)
        {
            // Check for "do()"
            //var test = input.Substring(i, 4);
            if (i + 4 <= input.Length && input.Substring(i, 4) == "do()")
            {
                enabled = true;
            }

            // Check for "don't()"
            //var blaat = input.Substring(i, 7);
            if (i + 7 <= input.Length && input.Substring(i, 7) == "don't()")
            {
                enabled = false;
            }

            if (i + 12 <= input.Length)
            {
                var match = Regex.Match(input.Substring(i, 12), @"mul\((\d+),(\d+)\)");
                if (match.Success)
                {
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);

                    p1 += x * y;
                    if (enabled)
                    {
                        p2 += x * y;
                    }

                    i += match.Length - 1;
                }
            }
            
        }

        //Console.WriteLine($"without do/don't: {p1}");
        //Console.WriteLine($"with do/don't: {p2}");






        return p2;
    }

}
