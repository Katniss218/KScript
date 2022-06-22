namespace KScript.Language.InputLanguage.Infrastructure.CST
{
    public class BinaryExpression : SyntaxNode
    {
        public SyntaxNode Left { get; set; }
        public BinaryOperator Op { get; set; }
        public SyntaxNode Right { get; set; }


        public override bool Equals( SyntaxNode obj )
        {
            if( obj as BinaryExpression == null )
            {
                return false;
            }

            BinaryExpression other = (BinaryExpression)obj;

            return this.Left.Equals( other.Left )
                && this.Op == other.Op
                && this.Right.Equals( other.Right );
        }
    }
}