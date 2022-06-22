using KScript.Language.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KScript.Language.Tests.LexerTests
{
    [TestClass]
    public class Text
    {
        [TestMethod]
        public void LetterW()
        {
            Lexer l = new Lexer( "W" );
            l.Lex();
            List<SyntaxToken> tokens = l.GetTokens();

            List<SyntaxToken> refTokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Text, "W", null),
            };

            Assert.IsTrue( tokens.SequenceEqual( refTokens, new SyntaxTokenTypeValueComparer() ) );
        }

        [TestMethod]
        public void WordSavannah()
        {
            Lexer l = new Lexer( "Savannah" );
            l.Lex();
            List<SyntaxToken> tokens = l.GetTokens();

            List<SyntaxToken> refTokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Text, "Savannah", null),
            };

            Assert.IsTrue( tokens.SequenceEqual( refTokens, new SyntaxTokenTypeValueComparer() ) );
        }

        [TestMethod]
        public void WordSav218()
        {
            Lexer l = new Lexer( "Sav218" );
            l.Lex();
            List<SyntaxToken> tokens = l.GetTokens();

            List<SyntaxToken> refTokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Text, "Sav218", null),
            };

            Assert.IsTrue( tokens.SequenceEqual( refTokens, new SyntaxTokenTypeValueComparer() ) );
        }

        [TestMethod]
        public void WordSav218HelloThere()
        {
            Lexer l = new Lexer( "Sav218HelloThere" );
            l.Lex();
            List<SyntaxToken> tokens = l.GetTokens();

            List<SyntaxToken> refTokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Text, "Sav218HelloThere", null),
            };

            Assert.IsTrue( tokens.SequenceEqual( refTokens, new SyntaxTokenTypeValueComparer() ) );
        }
    }
}
