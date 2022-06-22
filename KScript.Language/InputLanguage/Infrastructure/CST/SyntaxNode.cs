using System;

namespace KScript.Language.InputLanguage.Infrastructure.CST
{
    public abstract class SyntaxNode : IEquatable<SyntaxNode>
    {
        public abstract bool Equals( SyntaxNode other );
    }
}