using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SaoViet.Portal.Infrastructure.Searching.Lucene;
using SaoViet.Portal.Infrastructure.Searching.Lucene.Internals;

namespace SaoViet.Portal.Infrastructure.Searching;

public static class Extension
{
    public static WebApplicationBuilder AddLuceneSearch(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(typeof(ILuceneService<>), typeof(LuceneService<>));
        return builder;
    }
}