using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace Portal.Application.Search;

public class LuceneService : ILuceneService
{
    private readonly string _indexPathDirectory;

    public LuceneService(string indexPath) 
        => _indexPathDirectory = indexPath ?? throw new ArgumentNullException(nameof(indexPath));

    public void Index(IDictionary<string, List<Document>> document)
    {
        using var directory = FSDirectory.Open(_indexPathDirectory);
        var analyzer = new VietnameseAnalyzer(LuceneVersion.LUCENE_48);
        var indexConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);
        using var writer = new IndexWriter(directory, indexConfig);
        var docs = document.SelectMany(item
                => item.Value.Select(doc
                    => new TextField(item.Key, doc.ToString(), Field.Store.YES)))
            .Select(field => new Document { field })
            .ToList();

        writer.AddDocuments(docs);

        writer.Flush(triggerMerge: false, applyAllDeletes: false);
    }

    public IEnumerable<Document> Search(string query, int maxResults)
    {
        using var directory = FSDirectory.Open(_indexPathDirectory);
        var analyzer = new VietnameseAnalyzer(LuceneVersion.LUCENE_48);
        var indexConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);
        using var writer = new IndexWriter(directory, indexConfig);
        var searcher = new IndexSearcher(writer.GetReader(applyAllDeletes: true));
        var parser = new QueryParser(LuceneVersion.LUCENE_48, "content", analyzer);
        var fuzzyQuery = new FuzzyQuery(new Term("content", query), 2);
        var queryParser = parser.Parse(query);
        var booleanQuery = new BooleanQuery
        {
            { queryParser, Occur.SHOULD },
            { fuzzyQuery, Occur.SHOULD }
        };
        var hits = searcher.Search(booleanQuery, maxResults).ScoreDocs;
        foreach (var hit in hits)
        {
            yield return searcher.Doc(hit.Doc);
        }
    }

    public void ClearAll()
    {
        using var directory = FSDirectory.Open(_indexPathDirectory);
        var analyzer = new VietnameseAnalyzer(LuceneVersion.LUCENE_48);
        var indexConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);
        using var writer = new IndexWriter(directory, indexConfig);
        writer.DeleteAll();
    }
}
