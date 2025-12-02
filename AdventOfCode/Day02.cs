using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly (string start, string end)[] _input;

    public Day02()
    {
        string inputRaw = File.ReadAllText(InputFilePath);
        string[] inputPairs = inputRaw.Split(",");
        _input = inputPairs
            .Select(x => (x.Split("-")[0], x.Split("-")[1]))
            .ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        long result = _input.Select(x => GetInvalidIdsSum(x.start, x.end)).Sum();
        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        long result = _input.Select(x => GetInvalidIdsSum2(x.start, x.end)).Sum();
        return new(result.ToString());
    }

    private long GetInvalidIdsSum(string start, string end)
    {
        long startId = long.Parse(start);
        long endId = long.Parse(end);

        long sum = 0;

        for (long i = startId; i <= endId; ++i)
        {
            string id = i.ToString();
            if (id.Length % 2 == 0 && id.Substring(0, id.Length / 2) == id.Substring(id.Length / 2))
            {
                sum += i;
            }
        }

        return sum;
    }

    private long GetInvalidIdsSum2(string start, string end)
    {
        long startId = long.Parse(start);
        long endId = long.Parse(end);

        long sum = 0;

        for (long i = startId; i <= endId; ++i)
        {
            string id = i.ToString();
            for (int j = 1; j <= id.Length / 2; ++j)
            {
                if (id.Length % j == 0)
                { 
                    int times = id.Length / j;
                    string pattern = id.Substring(0, j);
                    bool matches = true;
                    for (int k = 1; k < times; k++)
                    {
                        string next = id.Substring(j * k, j);
                        if (next != pattern)
                        {
                            matches = false;
                            break;
                        }
                    }

                    if (matches)
                    { 
                        sum += i;
                        break;
                    }
                }
            }
        }

        return sum;
    }
}
