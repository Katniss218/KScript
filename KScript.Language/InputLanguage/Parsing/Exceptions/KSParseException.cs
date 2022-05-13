using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Language.InputLanguage.Parsing.Exceptions
{
    public class KSParseException : Exception
    {
        public KSParseException() : base()
        {

        }

        public KSParseException( string message ) : base( message )
        {

        }

        public KSParseException( string message, Exception innerException ) : base( message, innerException )
        {

        }
    }
}