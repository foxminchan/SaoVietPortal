using Lucene.Net.Documents;

namespace Portal.Application.Search;

public interface ILuceneService
{
    public void Index(IEnumerable<Document> documents);
    public IEnumerable<Document> Search(string query, int maxResults);
}