namespace AdventOfCode;

public class Day08 : BaseDay
{
    private Node[] _nodes;

    private int _connections = 1000;

    public Day08()
    {
        string[] lines = File.ReadAllLines(InputFilePath);
        _nodes = lines
            .Select(l => new Node(long.Parse(l.Split(",")[0]), long.Parse(l.Split(",")[1]), long.Parse(l.Split(",")[2])))
            .ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        Dictionary<double, (Node a, Node b)> distances = CalculateDistances();

        var pairs = distances.OrderBy(kv => kv.Key).Take(_connections).Select(kv => kv.Value).ToList();
        var distinctNodes = pairs.SelectMany(p => new[] { p.a, p.b }).Distinct().ToList();
        List<HashSet<Node>> circuits = distinctNodes.Select(n => new HashSet<Node> { n }).ToList();

        foreach (var pair in pairs)
        {
            var circuitA = circuits.FirstOrDefault(c => c.Contains(pair.a));
            var circuitB = circuits.FirstOrDefault(c => c.Contains(pair.b));

            if (circuitA != circuitB)
            {
                circuitA.UnionWith(circuitB);
                circuits.Remove(circuitB);
            }
        }

        var ordered = circuits.OrderByDescending(c => c.Count).ToList();

        Console.WriteLine($"Top three largest circuits have sizes: {ordered[0].Count}, {ordered[1].Count}, {ordered[2].Count}");

        var res = ordered[0].Count * ordered[1].Count * ordered[2].Count;

        return new(res.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        Dictionary<double, (Node a, Node b)> distances = CalculateDistances();

        var pairs = distances.OrderBy(kv => kv.Key).Select(kv => kv.Value).ToList();
        List<HashSet<Node>> circuits = _nodes.Select(n => new HashSet<Node> { n }).ToList();

        long res = 0;
        foreach (var pair in pairs)
        {
            var circuitA = circuits.FirstOrDefault(c => c.Contains(pair.a));
            var circuitB = circuits.FirstOrDefault(c => c.Contains(pair.b));

            if (circuitA != circuitB)
            {
                circuitA.UnionWith(circuitB);
                circuits.Remove(circuitB);
            }

            if (circuits.Count == 1)
            {
                res = pair.a.X * pair.b.X;
                break;
            }
        }

        return new(res.ToString());
    }

    private double CalculateDistance(Node a, Node b)
    {
        return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2) + Math.Pow(a.Z - b.Z, 2));
    }

    private Dictionary<double, (Node a, Node b)> CalculateDistances()
    {
        Dictionary<double, (Node a, Node b)> distances = new();
        for (int i = 0; i < _nodes.Length - 1; ++i)
        {
            for (int j = i + 1; j < _nodes.Length; ++j)
            {
                var dist = CalculateDistance(_nodes[i], _nodes[j]);

                if (distances.ContainsKey(dist))
                {
                    throw new Exception("Duplicate distance found, not expected");
                }
                distances[dist] = (_nodes[i], _nodes[j]);
            }
        }
        return distances;
    }

    private record Node(long X, long Y, long Z);
}
