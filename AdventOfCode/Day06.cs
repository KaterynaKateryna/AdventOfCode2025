
namespace AdventOfCode;

public class Day06 : BaseDay
{
    private Problem[] _input;

    public Day06()
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

        _input = pivotedMatrix.Select(r => Problem.Parse(r)).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        long result = _input.Select(p => p.Solve()).Sum();
        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
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
