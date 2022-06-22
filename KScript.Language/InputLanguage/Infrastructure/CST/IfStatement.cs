using System;
using System.Collections.Generic;
using System.Linq;

namespace KScript.Language.InputLanguage.Infrastructure.CST
{
    public class DeclarationStatement : Statement
    {
        // DeclarationStatement
        //    : Identifier Identifier `;`
        // you need to declare a variable in a separate line before assigning a value to it. It's to simplify the language.
        public Variable Variable { get; set; }


        public override bool Equals( SyntaxNode obj )
        {
            if( obj as DeclarationStatement == null )
            {
                return false;
            }

            return this.Variable == ((DeclarationStatement)obj).Variable;
        }
    }
}