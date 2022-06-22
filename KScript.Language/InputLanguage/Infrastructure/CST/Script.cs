using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Language.InputLanguage.Infrastructure.CST
{
    /// <summary>
    /// Represents a .ks file.
    /// </summary>
    public class Script
    {
        public List<Function> Functions { get; set; } = new List<Function>();
    }
}
