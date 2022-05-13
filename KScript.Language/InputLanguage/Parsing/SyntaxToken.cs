using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KScript.Language.Parsing
{
    [DebuggerDisplay("{Type} {Value}")]
    public class SyntaxToken
    {
        /// <summary>
        /// Contains the information about the position of the first character of the token.
        /// </summary>
        public LineInfo LineInfo { get; set; }

        public SyntaxTokenType Type { get; set; }

        /// <summary>
        /// String representation of the token.
        /// </summary>
        public string Value { get; set; }

        public SyntaxToken( SyntaxTokenType type, string value, LineInfo lineInfo )
        {
            this.LineInfo = lineInfo;
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// Returns a new token that was created as a result of combining an ordered list of tokens
        /// </summary>
        public static SyntaxToken Combine( SyntaxTokenType newType, params SyntaxToken[] tokens )
        {
            StringBuilder sb = new StringBuilder();

            foreach( var token in tokens )
            {
                sb.Append( token.Value );
            }

            return new SyntaxToken( newType, sb.ToString(), tokens[0].LineInfo );
        }
    }

    public class SyntaxTokenTypeValueComparer : IEqualityComparer<SyntaxToken>
    {
        public bool Equals( SyntaxToken x, SyntaxToken y )
        {
            if( ReferenceEquals( x, y ) )
                return true;

            if( ReferenceEquals( x, null ) || ReferenceEquals( y, null ) )
                return false;

            return x.Type == y.Type && x.Value == y.Value;
        }

        public int GetHashCode( SyntaxToken x )
        {
            if( ReferenceEquals( x, null ) )
                return 0;

            return x.Type.GetHashCode() ^ x.Value.GetHashCode();
        }
    }
}