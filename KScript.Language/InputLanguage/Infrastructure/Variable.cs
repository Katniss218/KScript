

namespace KScript.Language.InputLanguage.Infrastructure
{
    public class Variable
    {
        public string Type { get; set; }

        public string Identifier { get; set; }

        public Variable( string type )
        {
            this.Type = type;
            this.Identifier = null;
        }

        public Variable( string type, string identifier )
        {
            this.Type = type;
            this.Identifier = identifier;
        }
    }
}