using Ardalis.SmartEnum;

namespace Portal.Shared.Infrastructure.Search.Lucene;

public sealed class LuceneAction : SmartEnum<LuceneAction>
{
    public static readonly LuceneAction Create = new(nameof(Create), 1);
    public static readonly LuceneAction Update = new(nameof(Update), 2);
    public static readonly LuceneAction Delete = new(nameof(Delete), 3);

    private LuceneAction(string name, int value) : base(name, value)
    {
    }
}