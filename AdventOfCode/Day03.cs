using System.Linq;
using System.Text;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly int[][] _input;

    public Day03()
    {
        string[] inputLines = File.ReadAllLines(InputFilePath);
        _input = inputLines
            .Select(l => l.Select(c => int.Parse(c.ToString())).ToArray())
            .ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        return new (_input.Sum(x => GetMaxJoltage(x, 2)).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(_input.Sum(x => GetMaxJoltage(x, 12)).ToString());
    }

    private long GetMaxJoltage(int[] bank, int batteriesCount)
    {
        StringBuilder sb = new();

        for (int i = 0; i < batteriesCount; ++i)
        {
            int max = bank.Take(bank.Length - batteriesCount + i + 1).Max();
            sb.Append(max.ToString());

            int indexOfMax = bank.IndexOf(max);
            
            bank = bank.Skip(indexOfMax + 1).ToArray();
        }

        return long.Parse(sb.ToString());
    }
}
