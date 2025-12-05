
namespace AdventOfCode;

public class Day05 : BaseDay
{
    private (long start, long end)[] _validRanges;
    private long[] _ids;

    public Day05()
    { 
        string[] lines = File.ReadAllLines(InputFilePath);

        int dividerIndex = lines.IndexOf(string.Empty);

        _validRanges = lines
            .Take(dividerIndex)
            .Select(l => (long.Parse(l.Split("-")[0]), long.Parse(l.Split("-")[1])))
            .ToArray();

        _ids = lines.Skip(dividerIndex + 1).Select(long.Parse).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        _validRanges = _validRanges.OrderBy(r => r.start).ToArray();
        _ids = _ids.OrderBy(id => id).ToArray();

        int freshCount = 0;
        foreach (long id in _ids)
        {
            int rangeIndex = 0;
            var nextRange = _validRanges[rangeIndex];
            while (id >= nextRange.start)
            {
                if (id <= nextRange.end)
                {
                    freshCount++;
                    break;
                }
                rangeIndex++;
                if (rangeIndex < _validRanges.Length)
                {
                    nextRange = _validRanges[rangeIndex];
                }
                else
                {
                    break;
                }
            }
        }

        return new(freshCount.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        List<(long start, long end)> processedRanges = new();
        long freshCount = 0;
        foreach (var range in _validRanges)
        {
            var deduplicated = DeduplicateRanges(range, processedRanges);
            if (deduplicated != null)
            {
                freshCount += deduplicated.Value.end - deduplicated.Value.start + 1;
                processedRanges.Add(deduplicated.Value);
            }
        }

        return new(freshCount.ToString());
    }

    private (long start, long end)? DeduplicateRanges((long start, long end) range, List<(long start, long end)> processedRanges)
    {
        foreach (var processedRange in processedRanges)
        {
            if (range.start >= processedRange.start && range.start <= processedRange.end)
            {
                if (range.end >= processedRange.start && range.end <= processedRange.end)
                {
                    return null; // this range fully duplicates an existing one
                }

                range.start = processedRange.end + 1; // trim the start of the range
            }
        }
        return range;
    }
}
