using KScript.Language.InputLanguage.Infrastructure;
using KScript.Language.InputLanguage.Infrastructure.CST;
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
            /*Lexer lex = new Lexer( @"15 * (10 + 20) + 3 / 10" );

            lex.Lex();

            List<SyntaxToken> tokens = lex.GetTokensNoWhiteSpaceNoComment();

            Parser p = new Parser( tokens );

            SyntaxNode node = p.Parse();*/

           // Lexer lex = new Lexer( @"{ abc = 5 + 5; def = 2 * 2; }" );
            Lexer lex = new Lexer( @"{ abc = a + 5; }" );

            lex.Lex();

            List<SyntaxToken> tokens = lex.GetTokensNoWhiteSpaceNoComment();

            Parser p = new Parser( tokens );

            SyntaxNode node = p.Parse();

            /*Lexer l = new Lexer( "2+2-2*2/2%2" );

            l.Lex();

            List<SyntaxToken> tokens = l.GetTokens();*/
        }
    }
}
