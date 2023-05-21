using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace SaoViet.Portal.Infrastructure.Searching.Lucene.Internals;

public class LuceneService<T> : ILuceneService<T> where T : class
{
    private readonly IndexWriter _indexWriter;
    private readonly IndexSearcher _indexSearcher;
    private readonly Analyzer _analyzer;
    private readonly QueryParser _queryParser;

    public LuceneService()
    {
        _analyzer = new VietnameseAnalyzer(LuceneVersion.LUCENE_48);
        _indexWriter = new IndexWriter(FSDirectory.Open("LuceneIndex"),
                       new IndexWriterConfig(LuceneVersion.LUCENE_48, _analyzer));
        _indexSearcher = new IndexSearcher(_indexWriter.GetReader(true));
        _queryParser = new QueryParser(LuceneVersion.LUCENE_48, "content", _analyzer);
    }

    public Dictionary<string, List<Document>> GetData(List<T> data)
    {
        var propertyIndex = new Dictionary<string, List<Document>>();

        foreach (var dummy in data.GetType().GetProperties())
        {
            foreach (var property in data.GetType().GetProperties())
            {
                if (!propertyIndex.ContainsKey(property.Name))
                    propertyIndex.Add(property.Name, new List<Document>());

                var value = property.GetValue(data, null);

                if (value is null) continue;

                var document = new Document
                    { new StringField(property.Name, value.ToString(), Field.Store.YES) };

                propertyIndex[property.Name].Add(document);
            }
        }

        return propertyIndex;
    }

    public void Index(List<T> data, int options)
    {
        var document = GetData(data);
        var docs = document.SelectMany(item
                => item.Value.Select(doc
                    => new TextField(item.Key, doc.ToString(), Field.Store.YES)))
            .Select(field => new Document { field })
            .ToList();

        switch (options)
        {
            case var _ when options == LuceneOptions.Create.Value:
                _indexWriter.AddDocuments(docs);
                break;

            case var _ when options == LuceneOptions.Update.Value:
                _indexWriter.UpdateDocuments(new Term("id", "1"), docs);
                break;

            case var _ when options == LuceneOptions.Delete.Value:
                _indexWriter.DeleteDocuments(new Term("id", "1"));
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(options), options, "Invalid option");
        }

        _indexWriter.Flush(false, false);
    }

    public bool IsExistIndex(T item)
        => _indexSearcher
            .Search(new QueryParser(LuceneVersion.LUCENE_48, "id", _analyzer)
                    .Parse(item.ToString()), 1)
            .ScoreDocs
            .Length > 0;

    public IEnumerable<Document> Search(string query, int maxResults)
    {
        var booleanQuery = new BooleanQuery
        {
            { _queryParser.Parse(query), Occur.SHOULD },
            { new FuzzyQuery(new Term("content", query), 2), Occur.SHOULD }
        };

        var hits = _indexSearcher.Search(booleanQuery, maxResults).ScoreDocs;

        foreach (var hit in hits)
            yield return _indexSearcher.Doc(hit.Doc);
    }

    public void ClearAll() => _indexWriter.DeleteAll();
}