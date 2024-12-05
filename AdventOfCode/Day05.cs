using Spectre.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string _input;
    private readonly List<string> _lines;
    private readonly List<List<string>> _chars;
    private readonly List<string> _part1;
    private readonly List<string> _part2;
    private readonly string input;
    private readonly List<(int, int)> checks = new List<(int, int)>();
    private readonly List<List<int>> updates = new List<List<int>>();

    public Day05()
    {
        _input = File.ReadAllText(InputFilePath);
        _lines = _input.Split("\n").ToList();


        var other = false;
        foreach (var line in _lines)
        {
            ////substring
            //_part1.Add(line.Substring(0, 1)); //first char
            //_part2.Add(line.Substring(1)); //rest

            if (string.IsNullOrEmpty(line))
            {
                other = true;
                continue;
            }

            if (other)
            {
                updates.Add(line.Split(',').Select(m => Int32.Parse(m)).ToList());

            }
            else
            {
                var parts = line.Split('|');
                checks.Add((Int32.Parse(parts[0]), Int32.Parse(parts[1])));
            }




        }
    }

    public override ValueTask<string> Solve_1() => new($"{Solve1()}");

    public override ValueTask<string> Solve_2() => new($"{Solve2()}");


    public int Solve1()
    {
        var runningtotal = 0;
        foreach (var update in updates)
        {
            if (IsUpdateGood(update))
            {
                runningtotal += update[update.Count / 2];
            }
        }

        return runningtotal;
    }


    public bool IsUpdateGood(List<int> update)
    {
        var status = true;
        for (var i = 0; i < update.Count; i++)
        {
            for (var j = i; j < update.Count; j++)
            {
                if (update[i] == update[j])
                { continue; }

                if (checks.Contains((update[j], update[i])))
                {
                    status = false;
                    break;
                }

            }
        }

        return status;
    }




    public long Solve2()
    {
        var runningtotal = 0;
        var wrongupdates = new List<List<int>>();
        foreach (var update in updates)
        {
            if (!IsUpdateGood(update))
            {
                wrongupdates.Add(update);
            }
        }

        foreach (var update in wrongupdates)
        {
            var correctUpdate = SortUpdate(update);
            runningtotal += correctUpdate[correctUpdate.Count / 2];
        }

        return runningtotal;
    }

    public List<int> SortUpdate(List<int> update)
    {
        while (!IsUpdateGood(update))
        {
            //swap the first violating rile
            for (var i = 0; i < update.Count; i++)
            {
                for (var j = i; j < update.Count; j++)
                {
                    if (update[i] == update[j])
                    { continue; }

                    if (checks.Contains((update[j], update[i])))
                    {
                        int temp = update[i];
                        update[i] = update[j];
                        update[j] = temp;
                        break;
                    }

                }
            }

        }
        return update;
    }
}