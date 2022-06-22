using System;
using System.Collections.Generic;
using System.Linq;

namespace KScript.Language.InputLanguage.Infrastructure.CST
{
    public class ReturnStatement : Statement
    {
        /// <summary>
        /// List of the variable identifiers that you want to return.
        /// </summary>
        public List<string> Variables { get; set; }


        public override bool Equals( SyntaxNode obj )
        {
            if( obj as ReturnStatement == null )
            {
                return false;
            }

            return this.Variables.SequenceEqual( ((ReturnStatement)obj).Variables );
        }
    }
}