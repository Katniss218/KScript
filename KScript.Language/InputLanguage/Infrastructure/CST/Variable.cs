namespace KScript.Language.InputLanguage.Infrastructure.CST
{
    public class Variable : SyntaxNode
    {
        public string Type { get; set; }

        public Identifier Identifier { get; set; }

        public Variable( string type )
        {
            this.Type = type;
            this.Identifier = null;
        }

        public Variable( string type, Identifier identifier )
        {
            this.Type = type;
            this.Identifier = identifier;
        }

        public override bool Equals( SyntaxNode obj )
        {
            if( obj as Variable == null )
            {
                return false;
            }

            Variable other = (Variable)obj;

            return this.Type == other.Type
                && this.Identifier.Equals( other.Identifier );
        }
    }
}