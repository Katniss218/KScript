using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Language.InputLanguage.Infrastructure
{
    public class Function
    {
        public List<Variable> ReturnValues { get; set; } = new List<Variable>();

        public List<Variable> Parameters { get; set; } = new List<Variable>();

        public StatementList Body { get; set; }
    }
}
