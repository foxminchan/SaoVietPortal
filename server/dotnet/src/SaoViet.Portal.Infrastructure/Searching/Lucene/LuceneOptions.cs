using Ardalis.SmartEnum;

namespace SaoViet.Portal.Infrastructure.Searching.Lucene;

public sealed class LuceneOptions : SmartEnum<LuceneOptions>
{
    public static readonly LuceneOptions Create = new(nameof(Create), 1);
    public static readonly LuceneOptions Update = new(nameof(Update), 2);
    public static readonly LuceneOptions Delete = new(nameof(Delete), 3);

    private LuceneOptions(string name, int value) : base(name, value)
    {
    }
}