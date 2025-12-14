namespace AdventOfCode;

public class Day06 : BaseDay
{
    public override ValueTask<string> Solve_1()
    {
        string[] lines = File.ReadAllLines(InputFilePath);
        string[][] matrix = lines.Select(l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToArray();

        string[][] pivotedMatrix = new string[matrix[0].Length][];
        for (int i = 0; i < matrix[0].Length; ++i)
        {
            pivotedMatrix[i] = new string[matrix.Length];
        }

        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                pivotedMatrix[j][i] = matrix[i][j];
            }
        }

        Problem[] input = pivotedMatrix.Select(r => Problem.Parse(r)).ToArray();

        long result = input.Select(p => p.Solve()).Sum();
        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        string[] lines = File.ReadAllLines(InputFilePath);
        string[] pivoted = new string[lines[0].Length];

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                pivoted[j] += lines[i][j];
            }
        }

        List<Problem> problems = new List<Problem>();
        List<string> parts = new List<string>();
        string operand = string.Empty;
        foreach (var line in pivoted)
        {
            var trimmed = line.Trim();
            if (string.IsNullOrWhiteSpace(trimmed))
            {
                // next problem
                parts.Add(operand);
                problems.Add(Problem.Parse(parts.ToArray()));
                operand = string.Empty;
                parts.Clear();
                continue;
            }

            if (trimmed.EndsWith('+') || trimmed.EndsWith('*'))
            {
                parts.Add(trimmed.Substring(0, trimmed.Length - 1).Trim());
                operand = trimmed.Last().ToString();
            }
            else
            {
                parts.Add(trimmed);
            }
        }

        // last problem
        parts.Add(operand);
        problems.Add(Problem.Parse(parts.ToArray()));

        long result = problems.Select(p => p.Solve()).Sum();

        return new(result.ToString());
    }

    private record Problem(long[] Arguments, char Operand)
    { 
        public static Problem Parse(string[] parts)
        {
            long[] args = parts.Take(parts.Length - 1).Select(long.Parse).ToArray();
            char op = parts.Last()[0];
            return new Problem(args, op);
        }

        public long Solve()
        {
            long res = Arguments[0];
            foreach (long arg in Arguments.Skip(1))
            {
                if (Operand == '+')
                { 
                    res += arg;
                }
                else if (Operand == '*')
                {
                    res *= arg;
                }
                else
                {
                    throw new InvalidOperationException($"Unknown operand {Operand}");
                }
            }

            return res;
        }
    }
}
