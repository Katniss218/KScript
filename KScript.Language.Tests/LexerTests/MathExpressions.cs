using KScript.Language.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KScript.Language.Tests.LexerTests
{
    [TestClass]
    public class MathExpressions
    {
        [TestMethod]
        public void Exp2Plus2()
        {
            Lexer l = new Lexer( "2+2" );
            l.Lex();
            List<SyntaxToken> tokens = l.GetTokens();

            List<SyntaxToken> refTokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Integer, "2", null),
                new SyntaxToken(SyntaxTokenType.Plus, "+", null),
                new SyntaxToken(SyntaxTokenType.Integer, "2", null),
            };

            Assert.IsTrue( tokens.SequenceEqual( refTokens, new SyntaxTokenTypeValueComparer() ) );
        }

        [TestMethod]
        public void Exp2Plus2Minus2()
        {
            Lexer l = new Lexer( "2+2-2" );
            l.Lex();
            List<SyntaxToken> tokens = l.GetTokens();

            List<SyntaxToken> refTokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Integer, "2", null),
                new SyntaxToken(SyntaxTokenType.Plus, "+", null),
                new SyntaxToken(SyntaxTokenType.Integer, "2", null),
                new SyntaxToken(SyntaxTokenType.Minus, "-", null),
                new SyntaxToken(SyntaxTokenType.Integer, "2", null),
            };

            Assert.IsTrue( tokens.SequenceEqual( refTokens, new SyntaxTokenTypeValueComparer() ) );
        }

        [TestMethod]
        public void Exp2Plus2Minus2Times2By2Mod2()
        {
            Lexer l = new Lexer( "2+2-2*2/2%2" );
            l.Lex();
            List<SyntaxToken> tokens = l.GetTokens();

            List<SyntaxToken> refTokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Integer, "2", null),
                new SyntaxToken(SyntaxTokenType.Plus, "+", null),
                new SyntaxToken(SyntaxTokenType.Integer, "2", null),
                new SyntaxToken(SyntaxTokenType.Minus, "-", null),
                new SyntaxToken(SyntaxTokenType.Integer, "2", null),
                new SyntaxToken(SyntaxTokenType.Asterisk, "*", null),
                new SyntaxToken(SyntaxTokenType.Integer, "2", null),
                new SyntaxToken(SyntaxTokenType.Slash, "/", null),
                new SyntaxToken(SyntaxTokenType.Integer, "2", null),
                new SyntaxToken(SyntaxTokenType.Percent, "%", null),
                new SyntaxToken(SyntaxTokenType.Integer, "2", null),
            };

            Assert.IsTrue( tokens.SequenceEqual( refTokens, new SyntaxTokenTypeValueComparer() ) );
        }
    }
}
