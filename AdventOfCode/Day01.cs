namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;

    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        int position = 50;
        int result = 0;

        for (int i = 0; i < _input.Length; ++i)
        {
            int rotation = int.Parse(_input[i].Substring(1));
            if (_input[i][0] == 'R')
            {
                position += rotation;
            }
            else
            { 
                position -= rotation;
            }

            position = position % 100;

            if (position == 0)
            {
                result++;
            }
        }
        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2");
}
