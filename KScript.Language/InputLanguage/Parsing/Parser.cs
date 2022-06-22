using KScript.Language.InputLanguage.Infrastructure;
using KScript.Language.InputLanguage.Infrastructure.CST;
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

        /// <summary>
        /// Ensures that the current token is of the specified type, and consumes it.
        /// </summary>
        private string EatToken( SyntaxTokenType type )
        {
            if( this._currentToken.Type != type )
            {
                throw new KSParseException( $"Invalid token '{this._currentToken.Type}: \"{this._currentToken.Value}\"' at {this._currentToken.LineInfo}" );
            }
            string tokenVal = this._currentToken.Value;
            this._pos++;
            return tokenVal;
        }



        /// <summary>
        /// Parses the input tokens. Primary.
        /// </summary>
        public SyntaxNode Parse()
        {
            SyntaxNode node = EatStatementList();

            return node;
        }

        /// <summary>
        /// StatementList
        ///     : '{' Statement* '}'
        ///     ;
        /// </summary>
        public SyntaxNode EatStatementList()
        {
            List<Statement> statements = new List<Statement>();

            EatToken( SyntaxTokenType.OpenCurlyBracket );

            while( _currentToken.Type != SyntaxTokenType.CloseCurlyBracket )
            {
                Statement statementNode = EatStatement();
                statements.Add( statementNode );
            }

            EatToken( SyntaxTokenType.CloseCurlyBracket );

            return new StatementList() { Statements = statements };
        }

        /// <summary>
        /// Statement
        ///     : AssignmentStatement
        ///     ;
        /// </summary>
        public Statement EatStatement()
        {
            return EatAssignmentStatement();
        }

        /// <summary>
        /// AssignmentStatement
        ///     : Identifier '=' Expression `;`
        ///     ;
        /// </summary>
        public AssignmentStatement EatAssignmentStatement()
        {
            Identifier identifier = EatIdentifier();

            EatToken( SyntaxTokenType.Equals );

            SyntaxNode expression = EatExpr();

            EatToken( SyntaxTokenType.Semicolon );

            return new AssignmentStatement()
            {
                Identifier = identifier,
                Expression = expression
            };
        }

        /// <summary>
        /// Identifier
        ///     : [A-Za-z_] [A-Za-z0-9_]*
        ///     ;
        /// </summary>
        public Identifier EatIdentifier()
        {
            // TODO - not accurate
            string val = EatToken( SyntaxTokenType.Text );
            return new Identifier( val );
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
        ///     | Identifier
        ///     | '(' Expression ')'
        ///     ;
        /// </summary>
        public SyntaxNode EatFactor()
        {
            if( _currentToken.Type == SyntaxTokenType.Integer )
            {
                string val = _currentToken.Value;

                EatToken( SyntaxTokenType.Integer );

                return new Literal( LiteralType.Integer, val);
            }

            if( _currentToken.Type == SyntaxTokenType.Text )
            {
                string val = _currentToken.Value;

                EatToken( SyntaxTokenType.Text );

                return new Identifier( val );
            }

            // TODO - identifiers (variables)

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