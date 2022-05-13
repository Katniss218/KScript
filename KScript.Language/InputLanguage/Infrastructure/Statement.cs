using System.Collections.Generic;

namespace KScript.Language.InputLanguage.Infrastructure
{
    public abstract class Statement
    {

    }

    public class DeclarationStatement : Statement
    {
        // you need to declare a variable in a separate line before assigning a value to it. It's to simplify the language.
        public Variable Variable { get; set; }
    }

    public class ReturnStatement : Statement
    {
        /// <summary>
        /// List of the variable identifiers that you want to return.
        /// </summary>
        public List<string> Variables { get; set; }
    }

    public class AssignmentStatement : Statement
    {
        /// <summary>
        /// The variable identifier that you want to assign to.
        /// </summary>
        public string Variable { get; set; }

        public Expression Expression { get; set; }
    }

    public class IfStatement : Statement
    {

    }
}