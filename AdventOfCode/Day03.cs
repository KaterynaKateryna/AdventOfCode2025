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
        return new (_input.Sum(GetMaxJoltage).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }

    private int GetMaxJoltage(int[] bank)
    {
        int max = bank.Take(bank.Length - 1).Max();
        int indexOfMax = bank.IndexOf(max);

        int max2 = bank.Skip(indexOfMax + 1).Max();

        return int.Parse(max.ToString() + max2.ToString());

    }
}
