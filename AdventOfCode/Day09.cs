
namespace AdventOfCode;

public class Day09 : BaseDay
{
    private Position[] _tiles;

    public Day09()
    { 
        var lines = File.ReadAllLines(InputFilePath);
        _tiles = lines.Select(l => new Position(int.Parse(l.Split(",")[0]), int.Parse(l.Split(",")[1]))).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        long largestArea = 0;
        for (int i = 0; i < _tiles.Length - 1; ++i)
        {
            for (int j = i + 1; j < _tiles.Length; ++j)
            {
                long area = GetArea(_tiles[i], _tiles[j]);
                if (area > largestArea)
                {
                    largestArea = area;
                }
            }
        }

        return new(largestArea.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }

    private long GetArea(Position p1, Position p2)
    {
        return Math.Abs(p1.X - p2.X + 1) * Math.Abs(p1.Y - p2.Y + 1);
    }

    private record Position(long X, long Y);
}
