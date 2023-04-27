using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Portal.Domain.Enum;

namespace Portal.Application.Search;

public class LuceneService<T> : ILuceneService<T> where T : class
{
    private readonly IndexWriter _indexWriter;
    private readonly IndexSearcher _indexSearcher;
    private readonly Analyzer _analyzer;
    private readonly QueryParser _queryParser;

    public LuceneService()
    {
        var directory = FSDirectory.Open("Index");
        _analyzer = new VietnameseAnalyzer(LuceneVersion.LUCENE_48);
        var indexConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, _analyzer);
        _indexWriter = new IndexWriter(directory, indexConfig);
        _indexSearcher = new IndexSearcher(_indexWriter.GetReader(applyAllDeletes: true));
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

    public void Index(List<T> data, string options)
    {
        var document = GetData(data);
        var docs = document.SelectMany(item
                => item.Value.Select(doc
                    => new TextField(item.Key, doc.ToString(), Field.Store.YES)))
            .Select(field => new Document { field })
            .ToList();

        switch (options)
        {
            case nameof(LuceneOptions.Create):
                _indexWriter.AddDocuments(docs);
                break;
            case nameof(LuceneOptions.Update):
                _indexWriter.UpdateDocuments(new Term("id", "1"), docs);
                break;
            case nameof(LuceneOptions.Delete):
                _indexWriter.DeleteDocuments(new Term("id", "1"));
                break;
            default:                
                throw new ArgumentOutOfRangeException(nameof(options), options, null);
        }

        _indexWriter.Flush(triggerMerge: false, applyAllDeletes: false);
    }

    public bool IsExistIndex(T item)
    {
        var parser = new QueryParser(LuceneVersion.LUCENE_48, "id", _analyzer);
        var query = parser.Parse(item.ToString());
        var hits = _indexSearcher.Search(query, 1).ScoreDocs;
        return hits.Length > 0;
    }

    public IEnumerable<Document> Search(string query, int maxResults)
    {
        var fuzzyQuery = new FuzzyQuery(new Term("content", query), 2);
        var queryParser = _queryParser.Parse(query);
        var booleanQuery = new BooleanQuery
        {
            { queryParser, Occur.SHOULD },
            { fuzzyQuery, Occur.SHOULD }
        };
        var hits = _indexSearcher.Search(booleanQuery, maxResults).ScoreDocs;
        foreach (var hit in hits)
            yield return _indexSearcher.Doc(hit.Doc);
    }

    public void ClearAll() => _indexWriter.DeleteAll();
}
