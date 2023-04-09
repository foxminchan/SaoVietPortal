using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace Portal.Application.Search;

public class LuceneService : ILuceneService
{
    private readonly string _indexPath;

    public LuceneService(string indexPath) => _indexPath = indexPath;

    public void Index(IEnumerable<Document> documents)
    {
        using var directory = FSDirectory.Open(_indexPath);
        var analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);
        var indexConfig = new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer);
        using var writer = new IndexWriter(directory, indexConfig);

        foreach (var doc in documents)
            writer.AddDocument(doc);

        writer.Flush(triggerMerge: false, applyAllDeletes: false);
    }

    public IEnumerable<Document> Search(string query, int maxResults)
    {
        using var directory = FSDirectory.Open(_indexPath);
        var analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);
        var indexConfig = new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer);
        using var writer = new IndexWriter(directory, indexConfig);
        var parser = new QueryParser(Lucene.Net.Util.LuceneVersion.LUCENE_48, "title", analyzer);
        var queryObj = parser.Parse(query);
        var fuzzyQuery = new FuzzyQuery(new Term("title", query), 2);
        var booleanQuery = new BooleanQuery
        {
            { queryObj, Occur.SHOULD },
            { fuzzyQuery, Occur.SHOULD }
        };
        var searcher = new IndexSearcher(writer.GetReader(applyAllDeletes: true));
        var hits = searcher.Search(booleanQuery, maxResults).ScoreDocs;
        return hits.Select(hit => searcher.Doc(hit.Doc));
    }

    public void ClearAll()
    {
        using var directory = FSDirectory.Open(_indexPath);
        var analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);
        var indexConfig = new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer);
        using var writer = new IndexWriter(directory, indexConfig);
        writer.DeleteAll();
    }
}