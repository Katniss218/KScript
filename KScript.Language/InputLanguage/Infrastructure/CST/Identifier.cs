
namespace KScript.Language.InputLanguage.Infrastructure.CST
{
    public class Identifier : SyntaxNode
    {
        public string Value { get; set; }

        public Identifier( string value )
        {
            this.Value = value;
        }


        public override bool Equals( SyntaxNode obj )
        {
            if( obj as Identifier == null )
            {
                return false;
            }

            return this.Value == ((Identifier)obj).Value;
        }
    }
}