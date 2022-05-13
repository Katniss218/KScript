

namespace KScript.Language.InputLanguage.Infrastructure
{
    public abstract class SyntaxNode
    {

    }

    public abstract class Expression : SyntaxNode
    {

    }

    public class UnaryExpression : SyntaxNode
    {
        public UnaryOperator Op { get; set; }
        public SyntaxNode Node { get; set; }
    }
    
    public class BinaryExpression : SyntaxNode
    {
        public SyntaxNode Left { get; set; }
        public BinaryOperator Op { get; set; }
        public SyntaxNode Right { get; set; }
    }

    /// <summary>
    /// Basically a literal.
    /// </summary>
    public class ConstantExpression : SyntaxNode
    {
        public string Type { get; set; }

        public string Literal { get; set; }
    }

    // compiler-evaluated expression

    // variable expression
}