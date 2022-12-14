namespace KScript.Language.InputLanguage.Infrastructure.CST
{
    public enum LiteralType
    {
        Integer,
        String,
    }

    /// <summary>
    /// Represents a literal value of some literal type.
    /// </summary>
    public class Literal : SyntaxNode
    {
        public LiteralType Type { get; set; }

#warning TODO - int value?
        public string Value { get; set; }

        public Literal( LiteralType type, string value )
        {
            this.Type = type;
            this.Value = value;
        }


        public override bool Equals( SyntaxNode obj )
        {
            if( obj as Literal == null )
                return false;

            Literal other = (Literal)obj;

            return this.Type == other.Type
                && this.Value == other.Value;
        }
    }
}