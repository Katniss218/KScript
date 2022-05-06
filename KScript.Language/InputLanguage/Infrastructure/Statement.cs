

namespace KScript.Language.InputLanguage.Infrastructure
{
    public class Statement
    {

    }

    public class DeclarationStatement : Statement
    {
        public Variable Variable { get; set; }

        public Expression DefaultValue { get; set; }

        // default value assignment statement (expression). Constant is an expression?
    }
}