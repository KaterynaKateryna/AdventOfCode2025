
namespace AdventOfCode;

public class Day07 : BaseDay
{
    private char[][] _input;
    public Day07()
    {
        string[] lines = File.ReadAllLines(InputFilePath);
        _input = lines.Select(l => l.ToCharArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var start = new Position(0, _input[0].IndexOf('S'));
        HashSet<Position> beams = new() { start };
        int splits = 0;

        for (int i = 1; i < _input.Length; ++i)
        {
            HashSet<Position> newBeams = new();
            foreach (Position beam in beams)
            { 
                char nextChar = _input[i][beam.Y];
                if (nextChar == '^')
                { 
                    // split
                    splits++;

                    if (beam.Y > 0)
                    {
                        newBeams.Add(new Position(i, beam.Y - 1));
                    }
                    if (beam.Y < _input[0].Length - 1)
                    {
                        newBeams.Add(new Position(i, beam.Y + 1));
                    }
                }
                else
                {
                    newBeams.Add(new Position(i, beam.Y));
                }
            }
            beams = newBeams;
        }

        return new(splits.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }

    private record Position(int X, int Y);
}
