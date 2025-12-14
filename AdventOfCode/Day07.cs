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
        (int splits, _) = Solve();

        return new(splits.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        (_, long timelines) = Solve();

        return new(timelines.ToString());
    }

    private (int splits, long timelines) Solve()
    {
        var start = new Position(0, _input[0].IndexOf('S'));
        Dictionary<Position, long> beams = new() { { start, 1 } };
        int splits = 0;

        for (int i = 1; i < _input.Length; ++i)
        {
            Dictionary<Position, long> newBeams = new();
            foreach (Position beam in beams.Keys)
            {
                char nextChar = _input[i][beam.Y];
                if (nextChar == '^')
                {
                    // split
                    splits++;

                    if (beam.Y > 0)
                    {
                        var newPosition = new Position(i, beam.Y - 1);
                        TryAdd(newBeams, newPosition, beams[beam]);
                    }
                    if (beam.Y < _input[0].Length - 1)
                    {
                        var newPosition = new Position(i, beam.Y + 1);
                        TryAdd(newBeams, newPosition, beams[beam]);
                    }
                }
                else
                {
                    var newPosition = new Position(i, beam.Y);
                    TryAdd(newBeams, newPosition, beams[beam]);
                }
            }

            beams = newBeams;
        }

        return (splits, beams.Values.Sum());
    }

    private void TryAdd(Dictionary<Position, long> dictionary, Position newPosition, long timelines)
    {
        if (dictionary.ContainsKey(newPosition))
        {
            dictionary[newPosition] += timelines;
        }
        else
        {
            dictionary[newPosition] = timelines;
        }
    }

    private record Position(int X, int Y);
}
