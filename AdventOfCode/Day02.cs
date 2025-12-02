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
        return new("");
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
}
