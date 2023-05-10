using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Miscellaneous;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;

namespace Portal.Shared.Infrastructure.Search.Lucene.Internals;

public class VietnameseAnalyzer : global::Lucene.Net.Analysis.Analyzer
{
    private readonly LuceneVersion _version;

    public VietnameseAnalyzer(LuceneVersion version) => _version = version;

    protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
    {
        var tokenizer = new StandardTokenizer(_version, reader);
        TokenStream filter = new StandardFilter(_version, tokenizer);
        filter = new LowerCaseFilter(_version, filter);
        filter = new StopFilter(_version, filter, StandardAnalyzer.STOP_WORDS_SET);
        filter = new ASCIIFoldingFilter(filter);
        return new TokenStreamComponents(tokenizer, filter);
    }
}