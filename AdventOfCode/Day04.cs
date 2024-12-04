using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string _input;
    private readonly List<string> _lines;
    private readonly List<List<string>> _chars;
    private readonly List<string> _part1;
    private readonly List<string> _part2;
    private readonly string input;

    public Day04()
    {
        _input = File.ReadAllText(InputFilePath);
        _lines = _input.Split("\n").ToList();
        _chars = new List<List<string>>();
        foreach (var line in _lines)
        {
            var row = line.Select(x => x.ToString()).ToList();
            _chars.Add(row);
        }


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
        var permutations = new List<(int x, int y)>();
        //naar beneden
        permutations.Add((0, -1));
        //omhoog
        permutations.Add((0, 1));
        //naar rechts
        permutations.Add((1, 0));
        //naar links
        permutations.Add((-1, 0));
        //rechts omhoog
        permutations.Add((1, 1));
        //links omhoog
        permutations.Add((-1, 1));
        //links naar beneden
        permutations.Add((-1, -1));
        //rechts naar beneden
        permutations.Add((1, -1));

        var runningtotal = 0;


        for (var y = 0; y < _chars.Count; y++)
        {
            for (var x = 0; x < _chars[y].Count; x++)
            {
                if (x == 9 && y == 9)
                {
                    var rere = "sdfs";
                }

                if (_chars[y][x] == "X")
                {
                    runningtotal += CheckPermutations(_chars, permutations, (x, y));
                }

            }

        }

        return runningtotal;

    }


    public int CheckPermutations(List<List<string>> input, List<(int x, int y)> permutations, (int x, int y) startcord)
    {
        var runningtotal = 0;
        foreach (var permutation in permutations)
        {
            var word = new StringBuilder();
            word.Append("X");
            var x = startcord.x;
            var y = startcord.y;

            if ((x == 9 && y == 9) && permutation == (-1, 0))
            {
                var test = "sadfs";
            }

            for (int i = 0; i < 3; i++)
            {
                var newX = x + permutation.x;
                var newY = y + permutation.y;
                if (newX >= 0 && newY >= 0)
                {
                    if (newX < input.Count && newY < input[0].Count)
                    {
                        word.Append(input[y + permutation.y][x + permutation.x].ToUpper());
                    }
                }
                x = newX;
                y = newY;
            }
            if (word.ToString() == "XMAS")
            {
                runningtotal++;
            }
        }

        return runningtotal;
    }

    public long Solve2()
    {


        var runningtotal = 0;
        for (var y = 0; y < _chars.Count; y++)
        {
            for (var x = 0; x < _chars[y].Count; x++)
            {
                //if (x == 9 && y == 9)
                //{
                //    var rere = "sdfs";
                //}

                if (_chars[y][x] == "A")
                {
                    runningtotal += CheckMAS(_chars, (x, y));
                }

            }

        }

        return runningtotal;
    }

    public int CheckMAS(List<List<string>> input, (int x, int y) startcord)
    {
        
        //linksboven en rechtonder
        var check1 = new (int x, int y)[] { (-1, 1), (1, -1) };

        //rechtsboven en linksonder
        var check2 = new (int x, int y)[] { (1, 1), (-1, -1) };

        if(Check(input, startcord, check1) && Check(input, startcord, check2))
        {
            return 1;
        }
        return 0;
    }


    public bool Check(List<List<string>> input, (int x, int y) startcord, (int x, int y)[] checks)
    {
        var word = new StringBuilder();
        foreach (var check in checks.ToList())
        {
            var x = startcord.x;
            var y = startcord.y;
           
                var newX = x + check.x;
                var newY = y + check.y;
                if (newX >= 0 && newY >= 0)
                {
                    if (newX < input.Count && newY < input[0].Count)
                    {
                        word.Append(input[y + check.y][x + check.x].ToUpper());
                    }
                }
            
        }

        if (word.ToString() == "MS" || word.ToString() == "SM")
        {
            return true;
        }

        return false;
    }

}
