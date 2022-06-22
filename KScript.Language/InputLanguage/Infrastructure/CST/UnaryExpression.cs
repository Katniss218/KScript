namespace KScript.Language.InputLanguage.Infrastructure.CST
{
    public class UnaryExpression : SyntaxNode
    {
        public UnaryOperator Op { get; set; }
        public SyntaxNode Node { get; set; }


        public override bool Equals( SyntaxNode obj )
        {
            if( obj as UnaryExpression == null )
            {
                return false;
            }

            UnaryExpression other = (UnaryExpression)obj;

            return this.Op == other.Op
                && this.Node.Equals( other.Node );
        }
    }
}