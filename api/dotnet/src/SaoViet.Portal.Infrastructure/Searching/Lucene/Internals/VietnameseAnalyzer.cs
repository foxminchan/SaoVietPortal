using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Miscellaneous;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;

namespace SaoViet.Portal.Infrastructure.Searching.Lucene.Internals;

public class VietnameseAnalyzer : Analyzer
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