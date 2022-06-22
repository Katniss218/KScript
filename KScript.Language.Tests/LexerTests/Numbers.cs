using KScript.Language.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KScript.Language.Tests.LexerTests
{
    [TestClass]
    public class Numbers
    {
        [TestMethod]
        public void Integer2()
        {
            Lexer l = new Lexer( "2" );
            l.Lex();
            List<SyntaxToken> tokens = l.GetTokens();

            List<SyntaxToken> refTokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Integer, "2", null),
            };

            Assert.IsTrue( tokens.SequenceEqual( refTokens, new SyntaxTokenTypeValueComparer() ) );
        }

        [TestMethod]
        public void Integer218()
        {
            Lexer l = new Lexer( "218" );
            l.Lex();
            List<SyntaxToken> tokens = l.GetTokens();

            List<SyntaxToken> refTokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Integer, "218", null),
            };

            Assert.IsTrue( tokens.SequenceEqual( refTokens, new SyntaxTokenTypeValueComparer() ) );
        }
    }
}
