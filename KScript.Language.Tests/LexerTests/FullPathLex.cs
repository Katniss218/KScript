using KScript.Language.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KScript.Language.Tests.LexerTests
{
    [TestClass]
    public class FullPathLex
    {
        [TestMethod]
        public void TestMethod1()
        {
            Lexer l = new Lexer( "func Add( int32 a, int32 b ) { return a + b; }" );

            l.Lex();

            List<SyntaxToken> tokens = l.GetTokens();

            List<SyntaxToken> refTokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Text, "func", null),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", null),
                new SyntaxToken(SyntaxTokenType.Text, "Add", null),
                new SyntaxToken(SyntaxTokenType.OpenParenthesis, "(", null),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", null),
                new SyntaxToken(SyntaxTokenType.Text, "int32", null),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", null),
                new SyntaxToken(SyntaxTokenType.Text, "a", null),
                new SyntaxToken(SyntaxTokenType.Comma, ",", null),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", null),
                new SyntaxToken(SyntaxTokenType.Text, "int32", null),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", null),
                new SyntaxToken(SyntaxTokenType.Text, "b", null),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", null),
                new SyntaxToken(SyntaxTokenType.CloseParenthesis, ")", null),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", null),
                new SyntaxToken(SyntaxTokenType.OpenCurlyBracket, "{", null),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", null),
                new SyntaxToken(SyntaxTokenType.Text, "return", null),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", null),
                new SyntaxToken(SyntaxTokenType.Text, "a", null),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", null),
                new SyntaxToken(SyntaxTokenType.Plus, "+", null),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", null),
                new SyntaxToken(SyntaxTokenType.Text, "b", null),
                new SyntaxToken(SyntaxTokenType.Semicolon, ";", null),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", null),
                new SyntaxToken(SyntaxTokenType.CloseCurlyBracket, "}", null),
            };

            Assert.IsTrue( tokens.SequenceEqual( refTokens, new SyntaxTokenTypeValueComparer() ) );
        }
    }
}
