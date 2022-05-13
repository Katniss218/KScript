using KScript.Language.InputLanguage.Infrastructure;
using KScript.Language.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.TestConsole
{
    public class CompilerStuff
    {
        public static void Function()
        {
            Lexer lex = new Lexer( @"15 */ (10 + 20) + 3 / 10" );

            lex.Lex();

            List<SyntaxToken> tokens = lex.GetTokensNoWhiteSpace();

            Parser p = new Parser( tokens );

            SyntaxNode node = p.Parse();
        }
    }
}
