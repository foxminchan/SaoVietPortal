using Lucene.Net.Documents;

namespace SaoViet.Portal.Infrastructure.Searching.Lucene;

public interface ILuceneService<T> where T : class
{
    public bool IsExistIndex(T item);

    public Dictionary<string, List<Document>> GetData(List<T> data);

    public IEnumerable<Document> Search(string query, int maxResults);

    public void ClearAll();

    public void Index(List<T> data, int options);
}