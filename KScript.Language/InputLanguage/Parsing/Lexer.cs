using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Language.Parsing
{
    public class Lexer
    {
        private List<SyntaxToken> _lexed;

        public string S { get; private set; }

        private int _pos;

        public Lexer( string s )
        {
            this.S = s;
            this._pos = 0;
            this._lexed = new List<SyntaxToken>();
        }

        private void Advance( int step = 1 )
        {
            this._pos += step;
        }

        private bool EOF()
        {
            return _pos > S.Length - 1; // Beyond the last valid index into the string.
        }

        private char _currentChar => this.S[this._pos];

        private char Peek()
        {
            return this.S[this._pos + 1];
        }

        private string Peek( int length, int ahead = 1 )
        {
            return this.S.Substring( this._pos + ahead, length );
        }

        /// <summary>
        /// Adds a normal typical token.
        /// </summary>
        /// <param name="type">The type of the token to add.</param>
        /// <param name="length">The length of the string that shall be contained in the token</param>
        private void AddToken( SyntaxTokenType type, int length )
        {
            string substr = this.S.Substring( _pos, length );

            SyntaxToken token = new SyntaxToken( type, substr, LineInfo.Calculate( null, this.S, this._pos ) );

            _lexed.Add( token );

            Advance( length );
        }

        /// <summary>
        /// Single-iteration (primitive) tokens.
        /// </summary>
        private void FirstPass()
        {
            while( !EOF() )
            {
                switch( _currentChar )
                {
                    case '[':
                        AddToken( SyntaxTokenType.OpenSquareBracket, 1 );
                        break;
                    case ']':
                        AddToken( SyntaxTokenType.CloseCurlyBracket, 1 );
                        break;
                    case '{':
                        AddToken( SyntaxTokenType.OpenCurlyBracket, 1 );
                        break;
                    case '}':
                        AddToken( SyntaxTokenType.CloseCurlyBracket, 1 );
                        break;
                    case '(':
                        AddToken( SyntaxTokenType.OpenParenthesis, 1 );
                        break;
                    case ')':
                        AddToken( SyntaxTokenType.CloseParenthesis, 1 );
                        break;
                    case '\r':
                        if( Peek() == '\n' )
                            AddToken( SyntaxTokenType.NewLine, 2 );
                        else
                            // todo - Missing token for a \CR char.
                            break;
                        break;
                    case '\n':
                        AddToken( SyntaxTokenType.NewLine, 1 );
                        break;
                    case '.':
                        AddToken( SyntaxTokenType.Dot, 1 );
                        break;
                    case '+':
                        AddToken( SyntaxTokenType.Plus, 1 );
                        break;
                    case '-':
                        AddToken( SyntaxTokenType.Minus, 1 );
                        break;
                    case ';':
                        AddToken( SyntaxTokenType.Semicolon, 1 );
                        break;
                    case '/':
                        if( Peek() == '/' )
                            AddToken( SyntaxTokenType.DoubleSlash, 2 );
                        else if( Peek() == '*' )
                            AddToken( SyntaxTokenType.SlashAsterisk, 2 );
                        else
                            AddToken( SyntaxTokenType.Slash, 1 );
                        break;
                    case '*':
                        if( Peek() == '/' )
                            AddToken( SyntaxTokenType.AsteriskSlash, 2 );
                        else
                            AddToken( SyntaxTokenType.Asterisk, 1 );
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        AddToken( SyntaxTokenType.Digit, 1 );
                        break;
                    case ' ':
                        AddToken( SyntaxTokenType.WhiteSpace, 1 );
                        break;

                    default:
                        AddToken( SyntaxTokenType.Text, 1 );
                        break;

                }
            }
        }

        private SyntaxToken CombineStreak( SyntaxTokenType type, SyntaxTokenType newType )
        {
            int start = this._pos;

            // Seek to the end of the consecutive tokens.
            while( this._pos < this._lexed.Count && this._lexed[this._pos].Type == type )
            {
                this._pos++;
            }

            // Combine the found streak into one token.
            SyntaxToken[] streakTokens = this._lexed.SubArray( start, this._pos - start );

            SyntaxToken combinedToken = SyntaxToken.Combine( newType, streakTokens );

            return combinedToken;
        }

        /// <summary>
        /// Combines primitive tokens into hybrids and compounds.
        /// </summary>
        private void SecondPass()
        {
            List<SyntaxToken> newList = new List<SyntaxToken>();

            while( true )
            {
                for( this._pos = 0; this._pos < this._lexed.Count; /*empty*/ )
                {
                    if( this._lexed[this._pos].Type == SyntaxTokenType.WhiteSpace )
                    {
                        newList.Add( CombineStreak( SyntaxTokenType.WhiteSpace, SyntaxTokenType.WhiteSpace ) );
                        continue;
                    }
                    if( this._lexed[this._pos].Type == SyntaxTokenType.Text )
                    {
                        newList.Add( CombineStreak( SyntaxTokenType.Text, SyntaxTokenType.Text ) );
                        continue;
                    }
                    if( this._lexed[this._pos].Type == SyntaxTokenType.Digit )
                    {
                        newList.Add( CombineStreak( SyntaxTokenType.Digit, SyntaxTokenType.Integer ) );
                        continue;
                    }

                    newList.Add( this._lexed[this._pos] );
                    this._pos++; // only increment if it didn't find a streak.
                }

                // If nothing was changed, exit.
                if( newList.SequenceEqual( this._lexed, new SyntaxTokenTypeValueComparer() ) )
                {
                    return;
                }

                this._lexed = newList;
                newList = new List<SyntaxToken>();
            }
        }

        public void Lex()
        {
            FirstPass();

            SecondPass();
        }

        public List<SyntaxToken> GetTokens()
        {
            return this._lexed.ToList();
        }

        public List<SyntaxToken> GetTokensNoWhiteSpace()
        {
            return this._lexed.Where( t => t.Type != SyntaxTokenType.WhiteSpace ).ToList();
        }
    }
}