using KScript.Language.InputLanguage.Infrastructure;
using KScript.Language.InputLanguage.Parsing.Exceptions;
using System;
using System.Collections.Generic;

namespace KScript.Language.Parsing
{
    public class Parser
    {
        public List<SyntaxToken> Tokens { get; set; }
        private int _pos;

        public Parser( List<SyntaxToken> tokens )
        {
            this.Tokens = tokens;
            this._pos = 0;
        }

        private SyntaxToken _currentToken => this._pos >= this.Tokens.Count ? new SyntaxToken( SyntaxTokenType.NULL, null, null ) : this.Tokens[this._pos];

        private void EatToken( SyntaxTokenType type )
        {
            if( this._currentToken.Type != type )
            {
                throw new KSParseException( $"Invalid token '{this._currentToken.Type}: \"{this._currentToken.Value}\"' at {this._currentToken.LineInfo}" );
            }
            this._pos++;
        }

        /// <summary>
        /// Parses the input into a syntax tree.
        /// </summary>
        public SyntaxNode Parse()
        {
            SyntaxNode node = EatExpr();

            return node;
        }

        /// <summary>
        /// Expression
        ///     : Term (('+'|'-') Term)*
        ///     ;
        /// </summary>
        public SyntaxNode EatExpr()
        {
            SyntaxNode node = EatTerm();

            // Nest the nodes
            while( _currentToken.Type == SyntaxTokenType.Plus || _currentToken.Type == SyntaxTokenType.Minus )
            {
                BinaryOperator op = BinaryOperator.Add;

                if( _currentToken.Type == SyntaxTokenType.Plus )
                {
                    op = BinaryOperator.Add;
                    EatToken( SyntaxTokenType.Plus );
                }
                else if( _currentToken.Type == SyntaxTokenType.Minus )
                {
                    op = BinaryOperator.Sub;
                    EatToken( SyntaxTokenType.Minus );
                }
                node = new BinaryExpression()
                {
                    Left = node,
                    Op = op,
                    Right = EatTerm()
                };
            }
            return node;
        }

        /// <summary>
        /// Term
        ///     : Factor (('*'|'/'|'%') Factor)*
        ///     ;
        /// </summary>
        public SyntaxNode EatTerm()
        {
            SyntaxNode node = EatFactor();

            while( _currentToken.Type == SyntaxTokenType.Asterisk || _currentToken.Type == SyntaxTokenType.Slash )
            {
                BinaryOperator op = BinaryOperator.Mul;

                if( _currentToken.Type == SyntaxTokenType.Asterisk )
                {
                    op = BinaryOperator.Mul;
                    EatToken( SyntaxTokenType.Asterisk );
                }
                else if( _currentToken.Type == SyntaxTokenType.Slash )
                {
                    op = BinaryOperator.Div;
                    EatToken( SyntaxTokenType.Slash );
                }
                else if( _currentToken.Type == SyntaxTokenType.Percent )
                {
                    op = BinaryOperator.Mod;
                    EatToken( SyntaxTokenType.Percent );
                }
                node = new BinaryExpression()
                {
                    Left = node,
                    Op = op,
                    Right = EatFactor()
                };
            }
            return node;
        }

        /// <summary>
        /// Factor
        ///     : Integer
        ///     | '(' Expression ')'
        ///     ;
        /// </summary>
        public SyntaxNode EatFactor()
        {
            if( _currentToken.Type == SyntaxTokenType.Integer )
            {
                string val = _currentToken.Value;

                EatToken( SyntaxTokenType.Integer );

                return new ConstantExpression()
                {
                    Type = "Integer",
                    Literal = val
                };
            }

            if( _currentToken.Type == SyntaxTokenType.OpenParenthesis )
            {
                EatToken( SyntaxTokenType.OpenParenthesis );

                SyntaxNode expr = EatExpr();

                EatToken( SyntaxTokenType.CloseParenthesis );

                return expr;
            }

            throw new KSParseException( $"Invalid token '{this._currentToken.Type}: \"{this._currentToken.Value}\"' at {this._currentToken.LineInfo}" );
        }
    }
}