using KScript.Language.InputLanguage.Infrastructure;
using KScript.Language.InputLanguage.Infrastructure.CST;
using KScript.Language.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KScript.Language.Tests.ParserTests
{
    [TestClass]
    public class FullPathParse
    {
        [TestMethod]
        public void Test()
        {
            List<SyntaxToken> tokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.OpenCurlyBracket, "{", null),
                new SyntaxToken(SyntaxTokenType.Text, "Abc", null),
                new SyntaxToken(SyntaxTokenType.Equals, "=", null),
                new SyntaxToken(SyntaxTokenType.Text, "a", null),
                new SyntaxToken(SyntaxTokenType.Plus, "+", null),
                new SyntaxToken(SyntaxTokenType.Text, "b", null),
                new SyntaxToken(SyntaxTokenType.Minus, "-", null),
                new SyntaxToken(SyntaxTokenType.Integer, "123", null),
                new SyntaxToken(SyntaxTokenType.Semicolon, ";", null),
                new SyntaxToken(SyntaxTokenType.CloseCurlyBracket, "}", null),
            };

            Parser p = new Parser( tokens );

            var statementList = p.EatStatementList();

            SyntaxNode refNode = new StatementList()
            {
                Statements = new List<Statement>()
                {
                    new AssignmentStatement()
                    {
                        Identifier = new Identifier("Abc"),
                        Expression = new BinaryExpression()
                        {
                            Left = new BinaryExpression()
                            {
                                Left = new Identifier("a"),
                                Op = BinaryOperator.Add,
                                Right = new Identifier("b")
                            },
                            Op = BinaryOperator.Sub,
                            Right = new Literal( LiteralType.Integer, "123")
                        }
                    }
                }
            };

            Assert.IsTrue( statementList.Equals( refNode ) );
        }
    }
}