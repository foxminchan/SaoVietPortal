using Microsoft.Extensions.DependencyInjection;
using Portal.Shared.Infrastructure.Search.Lucene.Internals;
using Portal.Shared.Infrastructure.Search.Lucene;
using Microsoft.AspNetCore.Builder;

namespace Portal.Shared.Infrastructure.Search;

public static class Extension
{
    public static void AddLuceneSearch(this WebApplicationBuilder builder)
        => builder.Services.AddSingleton(typeof(ILuceneService<>), typeof(LuceneService<>));
}