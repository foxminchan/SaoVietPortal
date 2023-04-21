using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace Portal.Application.Search;

public class LuceneService : ILuceneService
{
    private readonly string _indexPath;

    public LuceneService(string indexPath) => _indexPath = indexPath;

    public void Index(IDictionary<string, List<Document>> document)
    {
        using var directory = FSDirectory.Open(_indexPath);
        var analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);
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
        using var directory = FSDirectory.Open(_indexPath);
        var analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);
        var indexConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);
        using var writer = new IndexWriter(directory, indexConfig);
        var parser = new QueryParser(LuceneVersion.LUCENE_48, "title", analyzer);
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
        var analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);
        var indexConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);
        using var writer = new IndexWriter(directory, indexConfig);
        writer.DeleteAll();
    }
}