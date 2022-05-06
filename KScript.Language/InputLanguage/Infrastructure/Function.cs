using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Language.InputLanguage.Infrastructure
{
    public class Function
    {
        public List<Variable> ReturnValues { get; set; }

        public List<Variable> Parameters { get; set; }

        public List<Statement> Body { get; set; }
    }
}
