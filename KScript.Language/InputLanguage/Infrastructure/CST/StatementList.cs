using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Language.InputLanguage.Infrastructure.CST
{
    public class StatementList : SyntaxNode
    {
        public List<Statement> Statements { get; set; }


        public override bool Equals( SyntaxNode obj )
        {
            if( obj as StatementList == null )
            {
                return false;
            }

            StatementList other = (StatementList)obj;

            if( this.Statements.Count != other.Statements.Count )
            {
                return false;
            }

            for( int i = 0; i < this.Statements.Count; i++ )
            {
                if( !this.Statements[i].Equals( other.Statements[i] ) )
                {
                    return false;
                }
            }

            return true;
        }
    }
}
