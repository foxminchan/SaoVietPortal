using Lucene.Net.Documents;

namespace Portal.Application.Search;

public interface ILuceneService
{
    public void Index(IDictionary<string, List<Document>> document);
    public IEnumerable<Document> Search(string query, int maxResults);
    public void ClearAll();
}