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

    public long Solve2()
    {
        var safecounter = new List<string>();
        var counter = 0;
        foreach (var line in _lines)
        {
            if (line == "72 69 66 64 61 59 56 59")
            {
                var test = "vladf";
            }

            //Console.WriteLine($"Doing {line}");
            var countchar = line.Split(' ').Count();
            for (var i = 0; i < countchar; i++)
            {
                if (safecounter.Contains(line))
                {
                    continue;
                }
                var allNumbers = line.Split(' ').ToList();
                allNumbers.RemoveAt(i);

                var previousnumber = 0;
                var safe = true;
                var firstline = true;

                var increasing = false;
                var decreasing = false;

                foreach (var item in allNumbers)
                {
                    if (String.Join(" ", allNumbers) == "72 69 66 64 61 59 56")
                    {
                        var test = "vladf";
                    }
                    if (firstline)
                    {
                        firstline = false;
                        previousnumber = Int32.Parse(item.ToString());
                        continue;
                    }
                    if (String.Join(" ", allNumbers) == "72 69 66 64 61 59 56")
                    {
                        var test = "vladf";
                    }

                    var difference = previousnumber - Int32.Parse(item.ToString());
                    var differenceAbs = Math.Abs(difference);
                    if (!(differenceAbs <= 3 && differenceAbs > 0))
                    {
                        safe = false;

                        break;
                    }


                    if (difference > 0)
                    {
                        decreasing = true;
                    }
                    else
                    {
                        increasing = true;
                    }
                    previousnumber = Int32.Parse(item.ToString());

                }
                if (safe)
                {
                    if ((decreasing != true && increasing == true) || (increasing != true && decreasing == true))
                    {
                        Console.WriteLine($"Line {line} is safe ({String.Join(" ", allNumbers)})");
                        safecounter.Add(line);
                        continue;
                    }else
                    {
                        //Console.WriteLine($"Line {line} is UNSAFE ({String.Join(" ", allNumbers)})");
                    }
                }
                else
                {
                    //Console.WriteLine($"Line {line} is UNSAFE ({String.Join(" ", allNumbers)})");
                }

            }


        }

        
        //safecounter.AddRange(helper());
        return safecounter.Distinct().Count();

    }

    public List<string> helper()
    {
        var safecounter = new List<string>();
        var counter = 0;
        foreach (var line in _lines)
        {
            var previousnumber = 0;
            var safe = true;
            var firstline = true;

            var increasing = false;
            var decreasing = false;
            var allNumbers = line.Split(' ').ToList();



            foreach (var item in line.Split(" "))
            {
                if (firstline)
                {
                    firstline = false;
                    previousnumber = Int32.Parse(item.ToString());
                    continue;
                }
                var difference = previousnumber - Int32.Parse(item.ToString());
                var differenceAbs = Math.Abs(difference);
                if (!(differenceAbs < 4 && differenceAbs > 0))
                {
                    safe = false;

                    break;
                }


                if (difference > 0)
                {
                    decreasing = true;
                }
                else
                {
                    increasing = true;
                }
                previousnumber = Int32.Parse(item.ToString());
                counter++;
            }

            if (safe)
            {
                if (decreasing != true || increasing != true)
                {
                    Console.WriteLine($"Line {line} is safe according to the old method");
                    safecounter.Add(line);
                }
            }

        }
        return safecounter;
    }


    public long Solve1()
    {
        var safecounter = 0;
        var counter = 0;
        foreach (var line in _lines)
        {
            var previousnumber = 0;
            var safe = true;
            var firstline = true;

            var increasing = false;
            var decreasing = false;
            var allNumbers = line.Split(' ').ToList();



            foreach (var item in line.Split(" "))
            {
                if (firstline)
                {
                    firstline = false;
                    previousnumber = Int32.Parse(item.ToString());
                    continue;
                }
                
                var difference = previousnumber - Int32.Parse(item.ToString());
                var differenceAbs = Math.Abs(difference);
                if (!(differenceAbs < 4 && differenceAbs > 0))
                {
                    safe = false;

                    break;
                }


                if (difference > 0)
                {
                    decreasing = true;
                }
                else
                {
                    increasing = true;
                }
                previousnumber = Int32.Parse(item.ToString());
                counter++;
            }

            if (safe)
            {
                if (decreasing != true || increasing != true)
                {
                    //Console.WriteLine($"Line {line} is safe");
                    safecounter++;
                }
            }

        }
        return safecounter;
    }
}
