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
    public class Expressions
    {
        [TestMethod]
        public void BinaryAdd_Variables()
        {
            List<SyntaxToken> tokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Text, "a", null),
                new SyntaxToken(SyntaxTokenType.Plus, "+", null),
                new SyntaxToken(SyntaxTokenType.Text, "b", null),
            };

            Parser p = new Parser( tokens );

            var statementList = p.EatExpr();

            SyntaxNode refNode = new BinaryExpression()
            {
                Left = new Identifier( "a" ),
                Op = BinaryOperator.Add,
                Right = new Identifier( "b" )
            };

            Assert.IsTrue( statementList.Equals( refNode ) );
        }


        [TestMethod]
        public void BinaryAdd_Literals()
        {
            List<SyntaxToken> tokens = new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Integer, "123", null),
                new SyntaxToken(SyntaxTokenType.Plus, "+", null),
                new SyntaxToken(SyntaxTokenType.Integer, "456", null),
            };

            Parser p = new Parser( tokens );

            var statementList = p.EatExpr();

            SyntaxNode refNode = new BinaryExpression()
            {
                Left = new Literal( LiteralType.Integer, "123" ),
                Op = BinaryOperator.Add,
                Right = new Literal( LiteralType.Integer, "456" )
            };

            Assert.IsTrue( statementList.Equals( refNode ) );
        }
    }
}