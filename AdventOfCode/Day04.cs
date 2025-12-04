
namespace AdventOfCode;

public class Day04 : BaseDay
{
    private char[][] _input;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath).Select(l => l.ToCharArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        int result = 0;
        for (int i = 0; i < _input.Length; ++i)
        { 
            for (int j = 0; j < _input[0].Length; ++j)
            {
                if (!IsRoll(_input[i][j]))
                {
                    continue;
                }
                int rollsAround = GetRollsAround(i, j);
                if (rollsAround < 4)
                { 
                    result++;
                }
            }
        }

        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }

    private int GetRollsAround(int row, int column)
    {
        int rolls = 0;

        if (row > 0 && column > 0)
        { 
            rolls += IsRoll(_input[row - 1][column - 1]) ? 1 : 0;
        }
        if (row > 0)
        { 
            rolls += IsRoll(_input[row - 1][column]) ? 1 : 0;
        }
        if (row > 0 && column < _input[0].Length - 1)
        { 
            rolls += IsRoll(_input[row - 1][column + 1]) ? 1 : 0;
        }
        if (column > 0)
        { 
            rolls += IsRoll(_input[row][column - 1]) ? 1 : 0;
        }
        if (column < _input[0].Length - 1)
        { 
            rolls += IsRoll(_input[row][column + 1]) ? 1 : 0;
        }
        if (row < _input.Length - 1 && column > 0)
        { 
            rolls += IsRoll(_input[row + 1][column - 1]) ? 1 : 0;
        }
        if (row < _input.Length - 1)
        { 
            rolls += IsRoll(_input[row + 1][column]) ? 1 : 0;
        }
        if (row < _input.Length - 1 && column < _input[0].Length - 1)
        { 
            rolls += IsRoll(_input[row + 1][column + 1]) ? 1 : 0;
        }

        return rolls;
    }

    private bool IsRoll(char c)
    {
        return c == '@';
    }
}
