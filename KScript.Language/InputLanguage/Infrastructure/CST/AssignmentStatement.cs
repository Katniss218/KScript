using System;
using System.Collections.Generic;
using System.Linq;

namespace KScript.Language.InputLanguage.Infrastructure.CST
{
    public class AssignmentStatement : Statement
    {
        /// <summary>
        /// The variable identifier that you want to assign to.
        /// </summary>
        public Identifier Identifier { get; set; }

        public SyntaxNode Expression { get; set; }


        public override bool Equals( SyntaxNode obj )
        {
            if( obj as AssignmentStatement == null )
                return false;

            AssignmentStatement other = (AssignmentStatement)obj;

            return this.Identifier.Equals( other.Identifier )
                && this.Expression.Equals( other.Expression );
        }
    }
}