namespace lib.test;

public class CommentParserTest
{

    #region Blue Sky Tests

    [Test]
    public void TestBlueSky_Parse()
    {
        // setup
        const string text = "> Quoted text\n\n" +
                            "Reply\n\n" +
                            "> More quoted text\n\n" +
                            "Another reply\n\n" +
                            "> Even more quoted text\n\n" +
                            "for their own tastes.\n\n" +
                            "> Forever quoted text\n\n" +
                            "And another reply\n\n" +
                            "Second layer of reply\n\n" +
                            "Forever reply";

        // act
        var blocks = CommentParser.Parse(text);
        
        // assert
        Assert.That(blocks, Has.Length.EqualTo(10));
        Assert.Multiple(() =>
        {
            Assert.That(blocks.Count(b => b is QuoteBlock), Is.EqualTo(4));
            Assert.That(blocks.Count(b => b is ResponseBlock), Is.EqualTo(6));
            Assert.That((blocks[1] as ResponseBlock)?.ParentQuoteId, Is.EqualTo(blocks[0].Id));
            Assert.That((blocks[3] as ResponseBlock)?.ParentQuoteId, Is.EqualTo(blocks[2].Id));
            Assert.That((blocks[5] as ResponseBlock)?.ParentQuoteId, Is.EqualTo(blocks[4].Id));
            Assert.That((blocks[7] as ResponseBlock)?.ParentQuoteId, Is.EqualTo(blocks[6].Id));
            Assert.That((blocks[8] as ResponseBlock)?.ParentQuoteId, Is.EqualTo(blocks[6].Id));
            Assert.That((blocks[9] as ResponseBlock)?.ParentQuoteId, Is.EqualTo(blocks[6].Id));
        });
    }

    [Test]
    public void TestBlueSky_Parse_HtmlQuote()
    {
        // setup
        const string text = "&gt; Quoted text\n\n" +
                            "Reply\n\n" +
                            "&gt; More quoted text\n\n" +
                            "Another reply\n\n" +
                            "&gt; Even more quoted text\n\n" +
                            "for their own tastes.\n\n" +
                            "&gt;Forever quoted text\n\n" +
                            "And another reply\n\n" +
                            "Second layer of reply\n\n" +
                            "Forever reply";

        // act
        var blocks = CommentParser.Parse(text);
        
        // assert
        Assert.That(blocks, Has.Length.EqualTo(10));
        Assert.Multiple(() =>
        {
            Assert.That(blocks.Count(b => b is QuoteBlock), Is.EqualTo(4));
            Assert.That(blocks.Count(b => b is ResponseBlock), Is.EqualTo(6));
        });
    }

    [Test]
    public void TestBlueSky_Parse_NoQuotes()
    {
        // setup
        const string text = "Reply\n\n" +
                            "Another reply\n\n" +
                            "for their own tastes.\n\n" +
                            "And another reply\n\n" +
                            "Second layer of reply\n\n" +
                            "Forever reply";

        // act
        var blocks = CommentParser.Parse(text);
        
        // assert
        Assert.That(blocks, Has.Length.EqualTo(6));
        Assert.Multiple(() =>
        {
            Assert.That(blocks.Count(b => b is QuoteBlock), Is.EqualTo(0));
            Assert.That(blocks.Count(b => b is ResponseBlock), Is.EqualTo(6));
        });
    }

    [Test]
    public void TestBlueSky_Parse_NoSpaceAfterQuote()
    {
        // setup
        const string text = ">Quoted text\n\n" +
                            "Reply\n\n" +
                            ">More quoted text\n\n" +
                            "Another reply\n\n" +
                            ">Even more quoted text\n\n" +
                            "for their own tastes.\n\n" +
                            ">Forever quoted text\n\n" +
                            "And another reply\n\n" +
                            "Second layer of reply\n\n" +
                            "Forever reply";

        // act
        var blocks = CommentParser.Parse(text);
        
        // assert
        Assert.That(blocks, Has.Length.EqualTo(10));
        Assert.Multiple(() =>
        {
            Assert.That(blocks.Count(b => b is QuoteBlock), Is.EqualTo(4));
            Assert.That(blocks.Count(b => b is ResponseBlock), Is.EqualTo(6));
        });
    }

    #endregion

}